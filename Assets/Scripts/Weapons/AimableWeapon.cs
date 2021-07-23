using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent (typeof (PlayerInput))]
public abstract class AimableWeapon : MonoBehaviour
{
    // This bulk could rather be moved to a projectileWeapon subclass
    // Serialized Fields
    [SerializeField]
    private GameObject _projectilePrefab;
    [SerializeField]
    [Tooltip("The Point where the projectile will be spawned")]
    private Transform _muzzlePoint;
    [SerializeField]
    private PlayerInput _playerInput;

    // Private Fields
    // this should become the current aim position by using a Raycast
    private Vector3 currentMousePosition;
    private bool _fireButtonPressed;

    // Properties
    public GameObject ProjectilePrefab { get => _projectilePrefab; }
    protected Vector3 CurrentMousePosition { get => MouseAction.ReadValue<Vector2> (); }
    public Transform MuzzlePoint { get => _muzzlePoint;}

    // CachedProperties
    #region CachedPropertires
    // Automatically get the PlayerInput Component which has to be attached to the same GameObject, when it is needed
    //private PlayerInput _playerInput;
    //public PlayerInput PlayerInput
    //{
    //    get
    //    {
    //        if ( _playerInput == null )
    //            _playerInput = this.GetComponent<PlayerInput> ();

    //        return _playerInput;
    //    }
    //}

    private InputAction _mouseAction = null;
    public InputAction MouseAction
    {
        get
        {
            if ( _mouseAction == null )
                _mouseAction = _playerInput.actions["MousePosition"];

            return _mouseAction;
        }
    }

    private InputAction _fireAction = null;
    public InputAction FireAction
    {
        get
        {
            if ( _fireAction == null )
                _fireAction = _playerInput.actions["Fire"];

            return _fireAction;
        }
    }
    #endregion

    private void Start ()
    {
        if ( _playerInput == null )
            Debug.Log ($"{this.gameObject.name} PlayerInput is not set.");

        FireAction.performed += ShotsFired;
    }

    private void ShotsFired ( InputAction.CallbackContext obj )
    {
        _fireButtonPressed = true;
    }

    private void FixedUpdate ()
    {
        Debug.Log ("Fire: " + _fireButtonPressed);
        FrameUpdate (_fireButtonPressed, CurrentMousePosition);
    }

    protected abstract void FrameUpdate ( bool fireButtonPressed, Vector2 currentMousePosition );
}
