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
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            _rigidbody.AddForce(_thrust * Vector3.up);
        }
        if(Input.GetKeyDown(KeyCode.A))
	    {
            _rigidbody.AddForce(_thrust * -Vector3.forward);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            _rigidbody.AddForce(_thrust * Vector3.forward);
        }
        if(Input.GetKeyDown(KeyCode.W))
        {
            _rigidbody.AddForce(_thrust * -Vector3.right);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            _rigidbody.AddForce(_thrust * Vector3.right);
        }
    }
}
