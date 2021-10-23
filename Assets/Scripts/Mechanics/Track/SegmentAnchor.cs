using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentAnchor : MonoBehaviour {
    /* ----- PRIVATE MEMBERS ------------------------------------------------------------------------ */ 
    [SerializeField]
    private List<Segment> _nextSegments = new List<Segment>();
    [SerializeField] 
    private List<Segment> _previousSegments = new List<Segment>(); 
    [SerializeField]
    private float _trackWidth = 0.0f;

    private Track _track = null;

    /* ----- PROPERTIES ----------------------------------------------------------------------------- */
    public List<Segment> NextSegments {get => _nextSegments; set => _nextSegments = value;}
    public List<Segment> PreviousSegments {get => _previousSegments; set => _previousSegments = value;}
    public Vector3 Position {get => transform.position; set => transform.position = value;}
    public Quaternion Orientation {get => transform.rotation; set => transform.rotation = value;}
    public Track Track {get => _track; set => _track = value;}
    public float TrackWidth {get => _trackWidth; set => _trackWidth = value;}

    /* ----- STATIC ---------------------------------------------------------------------------------- */  
    public static SegmentAnchor Create(Track track, Segment previousSegment, Segment nextSegment, Vector3 position, Quaternion orientation) {
        // create gameobject and attach required components 
        GameObject obj = new GameObject("segmentAnchor", typeof(SegmentAnchor), typeof(SphereCollider));
        obj.transform.parent = track.transform;
        obj.layer = LayerMask.NameToLayer("Track");

        // setup sphere collider to half the snap distance 
        obj.GetComponent<SphereCollider>().radius = track.SnapDistance / 2.0f;

        // setup segment anchor 
        SegmentAnchor anchor = obj.GetComponent<SegmentAnchor>();
        if(previousSegment) anchor.PreviousSegments.Add(previousSegment);
        if(nextSegment) anchor.NextSegments.Add(nextSegment);
        anchor.Position = position;
        anchor.Orientation = orientation;
        anchor.TrackWidth = track.DefaultWidth;

        // store associated track 
        anchor._track = track;

        return anchor; 
    }

    /* ----- PUBLIC METHODS --------------------------------------------------------------------------- */
    public void Rebuild() {
        foreach(Segment segment in NextSegments) {
            if(segment) segment.Rebuild();
        }
    }

    public void SetActive() {
        _track.ActiveAnchor = this;
    }

    public void Join(SegmentAnchor other) { 
        // check if other anchor is next anchor 
        Segment segment = NextSegments.Find(segment => segment.AnchorOut == other);
        if(segment) {
            DestroyImmediate(segment.gameObject);
            other.PreviousSegments.Remove(segment);
            NextSegments.Remove(segment);
        }

        // check if other anchor is previous anchor 
        segment = PreviousSegments.Find(segment => segment.AnchorIn == other);
        if(segment) {
            DestroyImmediate(segment.gameObject);
            other.NextSegments.Remove(segment);
            PreviousSegments.Remove(segment);
        }

        // append segments to the other anchor 
        other.NextSegments.AddRange(NextSegments);
        other.PreviousSegments.AddRange(PreviousSegments);
        
        // make sure all next segments are attached to other anchor 
        foreach(Segment nextSegment in other.NextSegments) {
            nextSegment.AnchorIn = other;
        }

        // make sure all previous segments are attached to other anchor
        foreach(Segment previousSegment in other.PreviousSegments) {
            previousSegment.AnchorOut = other;
        }

        // delete this anchor 
        DestroyImmediate(gameObject);
    } 

    public void OnDrawGizmos() {
        #if UNITY_EDITOR 
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, 0.2f);

            // should we snap to another anchor
            Collider[] anchorColliders = Physics.OverlapSphere(Position, _track.SnapDistance / 2.0f, 1<<LayerMask.NameToLayer("Track"));
            foreach(Collider collider in anchorColliders) {
                if(collider && collider.gameObject != this.gameObject) {
                    Join(collider.GetComponent<SegmentAnchor>());
                }
            }
        #endif 
    }
}
