using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Segment : MonoBehaviour {
    /*----- PROTECTED ------------------------------------------------------------*/
    [SerializeField]
    protected SegmentAnchor _anchorIn = null;
    [SerializeField]
    protected SegmentAnchor _anchorOut = null;
    [SerializeField]
    protected Vector3 _orientationOffsetIn = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField]
    protected Vector3 _orientationOffsetOut = new Vector3(0.0f, 0.0f, 0.0f);

    /*----- PROPERTIES -----------------------------------------------------------*/
    public SegmentAnchor AnchorIn {get => _anchorIn; set => _anchorIn = value;}
    public SegmentAnchor AnchorOut {get => _anchorOut; set => _anchorOut = value;}
    public Quaternion OrientationOffsetIn {get => Quaternion.Euler(_orientationOffsetIn);}
    public Quaternion OrientationOffsetOut {get => Quaternion.Euler(_orientationOffsetOut);}


    /*----- STATIC ---------------------------------------------------------------*/
    static public Segment Create<T>(SegmentAnchor anchor) where T : Segment {
        // create game object with required components and position it 
        GameObject obj = new GameObject("segment", typeof(T), typeof(MeshFilter), typeof(MeshRenderer));
        obj.transform.position = anchor.transform.position;
        obj.transform.parent = anchor.transform.parent;

        // initialize segment 
        Segment segment = obj.GetComponent<T>();
        anchor.GetComponent<SegmentAnchor>().NextSegments.Add(segment);
        segment.AnchorIn = anchor;

        segment.CreateDefaultAnchor();
        segment.Build();

        return segment;
    }

    /*----- METHODS --------------------------------------------------------------*/   
    public virtual void Rebuild() {
        Build(); 
        //AnchorOut.Rebuild();     
    }

    // creates a mesh based on input and output anchors 
    public abstract void Build();

    // called on creation of a segment to initialize anchors. 
    public virtual void CreateDefaultAnchor() {
        SegmentAnchor anchor = SegmentAnchor.Create(AnchorIn.transform.parent.GetComponent<Track>(), this, null, AnchorIn.Position + AnchorIn.Orientation * new Vector3(0.0f, 0.0f, AnchorIn.Track.DefaultLength), AnchorIn.Orientation);
        AnchorOut = anchor;
    }
}
