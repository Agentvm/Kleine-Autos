using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driving : MonoBehaviour
{
    public Rigidbody _rigidbody; 
    public float _thrust = 0.0f;
    public float _torque = 0.0f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.ResetCenterOfMass();
    }

    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.Space)) 
        {
            _rigidbody.AddForce(_thrust * transform.up * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
	    {
            _rigidbody.AddTorque(transform.up * -_torque * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        { 
            _rigidbody.AddTorque(transform.up * _torque * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.W))
        {
            _rigidbody.AddForce(_thrust * transform.forward * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            _rigidbody.AddForce(_thrust * -transform.forward * Time.deltaTime);
        }
    }
}
