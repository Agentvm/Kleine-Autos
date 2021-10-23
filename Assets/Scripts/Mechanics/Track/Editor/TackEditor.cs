using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Track))]
public class TackEditor : Editor {
    public override void OnInspectorGUI() {
        base.DrawDefaultInspector();    
        Track track = (Track)target;

        GUILayout.BeginHorizontal(); 

        if(GUILayout.Button(track.tex1, GUILayout.Width(100), GUILayout.Height(100))) {
            track.appendSegment<BezierSegment>();
        }

        GUILayout.EndHorizontal();
    }
}
