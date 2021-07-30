using UnityEngine.Assertions;
using UnityEngine;

/// <summary>
/// Makes the camera follow the specified game object. The offset of the camera is constant 
/// as specified by its transform. 
/// </summary>
public class FollowingCamera : MonoBehaviour
{
    [SerializeField] 
    [Tooltip("The followed game object.")]
    private GameObject _followedGameObject = null;
    private Vector3 _cameraOffset = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        Assert.IsTrue(_followedGameObject != null);
        _cameraOffset = transform.position - _followedGameObject.transform.position;    // calculate offset from camera to game object 
    }

    void Update()
    {
        Assert.IsTrue(_followedGameObject != null);
        transform.position = _followedGameObject.transform.position + _cameraOffset;    // offset camera position from followed object position 
        transform.LookAt(_followedGameObject.transform, transform.up);                  // orient the camera towards the followed object 
    }
}
