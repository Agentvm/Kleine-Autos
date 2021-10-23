using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Track : MonoBehaviour {
    public Texture tex1;
    public Texture tex2;

    /* ----- PRIVATE MEMBERS -------------------------------------------------------------- */
    [SerializeField]
    private float _defaultWidth = 0.0f;
    [SerializeField]
    private float _defaultLength = 0.0f;
    [SerializeField]
    private float _snapDistance = 0.5f;

    [SerializeField]
    private SegmentAnchor _activeAnchor = null;  

    [SerializeField]
    private List<Segment> _segments = new List<Segment>();


    /* ----- PROPERTIES ------------------------------------------------------------------- */ 
    public float DefaultWidth {get => _defaultWidth;}
    public float DefaultLength {get => _defaultLength;}
    public float SnapDistance {get => _snapDistance;}
    public SegmentAnchor ActiveAnchor {get => _activeAnchor; set => _activeAnchor = value;}

    /* ----- PUBLIC METHODS --------------------------------------------------------------- */
    public void Rebuild() {
        foreach(Segment segment in _segments) {
            segment.Build();
        }



       // if(_segments.Count > 0) _segments[0].GetComponent<Segment>().AnchorIn.Rebuild();
    }
  
    public void appendSegment<T>() where T : Segment  {
        if(_activeAnchor == null)  {
            // TODO: here we should check if there is an anchor that is not connected yet, e.g., at a junction 
            _segments = new List<Segment>();
            _activeAnchor = SegmentAnchor.Create(this, null, null, transform.position, Quaternion.Euler(Vector3.forward));
        }

        Segment segment = Segment.Create<T>(_activeAnchor);
        _segments.Add(segment);
        _activeAnchor = segment.GetComponent<Segment>().AnchorOut; // TODO: refine -> only if null
    }
}
