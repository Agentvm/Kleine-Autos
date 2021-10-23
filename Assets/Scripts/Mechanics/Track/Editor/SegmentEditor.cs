using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Segment), true), CanEditMultipleObjects]
public class SegmentEditor : Editor {
    public override void OnInspectorGUI() {  
        // Button for appending new anchor 
        if(GUILayout.Button("Append anchor")) {
            Debug.Log("lol");
        }

        // draw default inspector and observe parameter changes 
        EditorGUI.BeginChangeCheck(); 
        base.DrawDefaultInspector();  
        if(EditorGUI.EndChangeCheck()) {
            // rebuild selected (updated) track segments 
            foreach(object obj in serializedObject.targetObjects) {
                Segment segment = (Segment)obj;
                segment.Rebuild();
            }
        }
    }
}
