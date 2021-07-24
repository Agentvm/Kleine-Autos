using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Use this base class to create Weapon behaviours
/// </summary>
public abstract class AimableWeapon : MonoBehaviour
{
    public const float MaxRaycastDistance = 120f;

    [SerializeField]
    [Tooltip("(optional) The Transform which to turn. Defaults to this")]
    private Transform _turningPoint;
    [SerializeField]
    [Tooltip("The Point where the projectile will be spawned")]
    private Transform _muzzlePoint;

    // Private Fields
    // this should become the current aim position by using a Raycast
    private Vector3 currentMousePosition;
    private bool _fireButtonPressed;
    private Ray _ray;
    private RaycastHit _raycastHit;


    // Properties
    public Transform MuzzlePoint { get => _muzzlePoint; }
    public Vector2 CurrentMousePosition { get => MouseAction.ReadValue<Vector2> (); }
    public bool FireButtonPressed { get => _fireButtonPressed; private set => _fireButtonPressed = value; }
    public Vector3 CurrentAimPosition
    {
        get
        {
            _ray = Camera.main.ScreenPointToRay (CurrentMousePosition);
            Physics.Raycast (_ray, out _raycastHit, MaxRaycastDistance);

            return _raycastHit.point;
        }
    }

    // CachedProperties
    #region CachedPropertires
    // When it is needed, automatically find the first PlayerInput Component in the scene (expensive)
    // This will fail, when the private field _playerInput is used instead of the Property PlayerInput below
    private PlayerInput _playerInput;
    public PlayerInput PlayerInput
    {
        get
        {
            if ( _playerInput == null )
                _playerInput = GameObject.FindObjectOfType<PlayerInput> ();

            return _playerInput;
        }
    }

    private InputAction _mouseAction = null;
    public InputAction MouseAction
    {
        get
        {
            if ( _mouseAction == null )
                _mouseAction = PlayerInput.actions["MousePosition"];

            return _mouseAction;
        }
    }

    private InputAction _fireAction = null;
    public InputAction FireAction
    {
        get
        {
            if ( _fireAction == null )
                _fireAction = PlayerInput.actions["Fire"];

            return _fireAction;
        }
    }
    #endregion

    private void Start ()
    {
        FireAction.performed += ShotsFired;
    }

    private void ShotsFired ( InputAction.CallbackContext obj )
    {
        FireButtonPressed = true;
    }

    private void Aim()
    {
        if ( _turningPoint == null )
            this.transform.LookAt (CurrentAimPosition);
        else
            _turningPoint.LookAt (CurrentAimPosition);
    }

    private void FixedUpdate ()
    {
        Aim ();
        FrameUpdate (FireButtonPressed, _raycastHit);

        // Reset variables for next frame
        FireButtonPressed = false;
        _raycastHit = default;
    }

    // Let the subclass implement the firing behaviour
    protected abstract void FrameUpdate ( bool fireButtonPressed, RaycastHit hitInfo );
}
