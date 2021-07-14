using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour
{
    public Rigidbody _rigidbody; 
    public float _thrust = 20;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space)) 
        {
            _rigidbody.AddForce(_thrust * Vector3.up);
        }
        if(Input.GetKey(KeyCode.A))
	    {
            _rigidbody.AddForce(_thrust * -Vector3.forward);
        }
        if(Input.GetKey(KeyCode.D))
        {
            _rigidbody.AddForce(_thrust * Vector3.forward);
        }
        if(Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(_thrust * -Vector3.right);
        }
        if(Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(_thrust * Vector3.right);
        }
    }
}
