using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameObject _followedGameObject = null;
    public Vector3 _distance = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {        
    }

    void Update()
    {
        if(_followedGameObject != null) 
        {
            transform.position = _followedGameObject.transform.position + _distance;    // offset camera position from followed object position 
            transform.LookAt(_followedGameObject.transform, transform.up);    // orient the camera towards the followed object 
        }     
    }
}
