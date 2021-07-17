using UnityEngine;
using UnityEngine.InputSystem;

public class Driving : MonoBehaviour
{
    public Rigidbody _rigidbody; 
    public float _thrust = 0.0f;

    private Vector2 _direction = new Vector2(0.0f, 0.0f);
    private bool _jumping = false;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.ResetCenterOfMass();
    }

    void OnMove(InputValue value) {
        _direction = value.Get<Vector2>();
        Debug.Log("Moving");
    }

    void OnJump() {
        _jumping = true;
        Debug.Log("Jumping");
    }

    void FixedUpdate() {
        if(_jumping) _rigidbody.AddForce(_thrust * transform.up * Time.deltaTime);
        _jumping = false;

        Vector3 direction = new Vector3(_direction.x, 0.0f, _direction.y);
        _rigidbody.AddForce(_thrust * direction * Time.deltaTime);
    }
}
