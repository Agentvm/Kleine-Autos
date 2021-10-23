using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FollowingCamera))]
public class FollowingCameraInspector : Editor
{
    public override void OnInspectorGUI() 
    {
        base.DrawDefaultInspector();
    }

    public void OnSceneGUI() {
        FollowingCamera cam = (FollowingCamera)target; 
        UnityEditor.Handles.DrawLine(cam.transform.position, cam._followedGameObject.transform.position, 5.0f);
    }
}
