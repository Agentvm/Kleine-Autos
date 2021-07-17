using UnityEngine;
using UnityEngine.InputSystem;

public class Driving : MonoBehaviour
{
    public Rigidbody _rigidbody; 
    public float _velocity = 0.0f;
    public float _torque = 0.0f;

    private Vector2 _direction = new Vector2(0.0f, 0.0f);
    private bool _jumping = false;
    private bool _grounded = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Vector3 massOffset = new Vector3(0.0f, 0.5f, 0.0f);
        _rigidbody.centerOfMass = _rigidbody.centerOfMass - massOffset;
        //_rigidbody.ResetCenterOfMass();
    }

    void OnMove(InputValue value) {
        _direction = value.Get<Vector2>();
        Debug.Log("Moving");
    }

    void OnJump() {
        _jumping = true;
        Debug.Log("Jumping");
    }

    void OnCollisionStay(Collision collisionInfo) {
        _grounded = true;
    }

    void FixedUpdate() {
        Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

        // Check for upward velocity 
        if(_jumping) velocity = velocity + 10 * _velocity * transform.up * Time.deltaTime;

        // Check for foward velocity 
        velocity = velocity + _velocity * _direction.y * transform.forward * Time.deltaTime;

        // Calculate angular velocity based on forward velocity 
        Vector3 angularVelocity = new Vector3(0.0f, velocity.normalized.magnitude * _torque * _direction.x * Time.deltaTime, 0.0f);

        // Penalty if not grounded 
        if(!_grounded) {
            velocity = velocity * 0.1f;
            angularVelocity = angularVelocity * 0.0f;
        }

        // Apply velocity to rigid body 
        _rigidbody.velocity = _rigidbody.velocity + velocity;
        //_rigidbody.angularVelocity = _rigidbody.angularVelocity  + angularVelocity;
        transform.Rotate(angularVelocity, Space.Self);

        // reset variables 
        _jumping = false;
        _grounded = false;
    }
}
