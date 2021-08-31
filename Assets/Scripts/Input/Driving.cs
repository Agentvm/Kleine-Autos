using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody))]
public class Driving : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Maximum speed [m/s]")]
    private float _maxSpeed = 0.0f;
    
    [SerializeField]
    [Tooltip("Turning speed [degree/s]")]
    private float _turningSpeed = 0.0f;
    
    [SerializeField]
    [Tooltip("Acceleration [m/s^2]")]
    private float _acceleration = 0.0f;
    
    [SerializeField] 
    [Tooltip("Penalty multiplicator for turning speed if not grounded")]
    private float _turningSpeedPenaltyNotGrounded = 0.0f;

    [SerializeField] 
    [Tooltip("Penalty multiplicator for velocity if not grounded")]
    private float _accelerationPenaltyNotGrounded = 0.0f;

    [SerializeField]
    [Tooltip ("Offset for the car turnpoint from object center")]
    private Vector3 _turnpointOffset = new Vector3 (0.0f, 0.0f, 0.0f);

    [SerializeField]
    [Tooltip ("Offset of the center of mass from the object center")]
    private Vector3 _centerOfMassOffset = new Vector3 (0.0f, 0.0f, 0.0f);

    private Rigidbody _rigidbody = null;
    private Vector2 _inputDirection = new Vector2(0.0f, 0.0f);
    private bool _inputJumping = false;
    private bool _isGrounded = false;

    private bool _steeringDisabled;
    public bool SteeringDisabled
    {
        get => _steeringDisabled;
        set
        {
            this._steeringDisabled = value;

            // Stop current motion
            if (_steeringDisabled)
            {
                this._rigidbody.velocity = Vector3.zero;
                this._inputDirection = Vector2.zero;
            }
        }
    }

    void Start()
    {
        // offset center of mass to make the car more stable 
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = _rigidbody.centerOfMass + _centerOfMassOffset;
    }

    void FixedUpdate() {
        // calculate actual velocity based on input direction 
        Vector3 directedAcceleration = _acceleration * _inputDirection.y * transform.forward * Time.deltaTime; 
        float directedTurningSpeed = _rigidbody.velocity.magnitude * _turningSpeed * _inputDirection.x * Time.deltaTime;

        // calculate a penalty if the car is not grounded 
        if(!_isGrounded) {
            directedAcceleration = directedAcceleration * _accelerationPenaltyNotGrounded;
            directedTurningSpeed = directedTurningSpeed * _turningSpeedPenaltyNotGrounded;
        }

        // apply rotation and velocity to rigidbody 
        _rigidbody.velocity = _rigidbody.velocity + directedAcceleration;
        transform.RotateAround(transform.position + _turnpointOffset, transform.up, directedTurningSpeed);

        // make sure speed does not exceed max speed
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _maxSpeed);

        // reset variables 
        _inputJumping = false;
        _isGrounded = false;
    }

    /// <summary>
    /// Event triggered from Unity InputManager. Receives a Vector2 with the direction as parameter. 
    /// </summary>
    void OnMove(InputValue value) {
        if (!SteeringDisabled)
            _inputDirection = value.Get<Vector2>();
    }

    /// <summary>
    /// Event triggered from Unity InputManager when the jump button is pressed.  
    /// </summary>
    void OnJump() {
        if (!SteeringDisabled)
            _inputJumping = true;
    }

    /// <summary>
    /// Event triggered when the car collides with an object, i.e., when it is grounded.   
    /// </summary>
    void OnCollisionStay(Collision collisionInfo) {
        _isGrounded = true;
    }
}
