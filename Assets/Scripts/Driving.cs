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
            _rigidbody.AddForce(_thrust * transform.up);
        }
        if(Input.GetKey(KeyCode.A))
	    {
            transform.RotateAround(transform.position, Vector3.up, -0.5f);
        }
        if(Input.GetKey(KeyCode.D))
        { 
            transform.RotateAround(transform.position, Vector3.up, 0.5f);
        }
        if(Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(_thrust * transform.forward);
        }
        if(Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(_thrust * -transform.forward);
        }
    }
}
