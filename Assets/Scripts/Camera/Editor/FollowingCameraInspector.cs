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
        if (!target)
            return;
        FollowingCamera cam = (FollowingCamera)target;
        if (!cam.FollowedGameObject)
            return;
        UnityEditor.Handles.DrawLine(cam.transform.position, cam.FollowedGameObject.transform.position, 5.0f);
    }
}
