using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SegmentAnchor))]
public class SegmentAnchorEditor : Editor {
    public override void OnInspectorGUI() {  
        base.DrawDefaultInspector();  
      
        // retrieve anchor and check if position changed 
        SegmentAnchor anchor = (SegmentAnchor)target;
        if(GUILayout.Button("set active")) {
            anchor.SetActive();
        }
        if(anchor.transform.hasChanged) {
            // rebuild connected segments 
            anchor.Track.Rebuild();
        }
    }
}
