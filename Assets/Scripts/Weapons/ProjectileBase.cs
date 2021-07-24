using UnityEngine.InputSystem;
using UnityEngine;
using System.Threading.Tasks;

/// <summary>
/// Use this base class to create Projectile behaviours
/// </summary>
[RequireComponent (typeof (Collider))]
public abstract class ProjectileBase : MonoBehaviour
{
    // Serialized Fields
    [SerializeField]
    [Range(0, 20)]
    [Tooltip("Damage when hitting another Player")]
    private int _damage = 6;
    [SerializeField]
    [Range (1f, 100f)]
    private float _projectileSpeed = 30f;
    [SerializeField]
    [Range(-1f, 10f)]
    [Tooltip("Time until projectile disappears")]
    private float _destructionTime = 6f;

    // Variables
    private float _currentTime = 0f;
    private int? _ownerPlayerIndex = null;

    // Properties
    public float ProjectileSpeed { get => _projectileSpeed; private set => _projectileSpeed = value; }
    public int Damage { get => _damage; private set => _damage = value; }
    public float DestructionTime { get => _destructionTime; private set => _destructionTime = value; }
    public int? OwnerPlayerIndex { get => _ownerPlayerIndex; set => _ownerPlayerIndex = value; }

    // CachedProperties
    #region CachedProperties
    private Collider _collider = null;
    public Collider Collider
    {
        get
        {
            if ( _collider == null )
                _collider = this.GetComponent<Collider> ();

            return _collider;
        }
    }
    #endregion

    private void Start ()
    {
        _currentTime = Time.time;
        //Collider.enabled = false; // Disable until collision with Weapon has ended
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if ( DestructionTime >= 0 && Time.time > _currentTime + DestructionTime )
            Destroy (this.transform.gameObject);

        FrameUpdate ();
    }

    // Let the subclass implement the exact behaviour at each frame
    protected abstract void FrameUpdate ();

    // Triggered by the attached Collider Component
    private async void OnCollisionEnter ( Collision collision )
    {
        if ( collision.transform.GetComponent<AimableWeapon> () )
            return;

        if ( this.OwnerPlayerIndex == null )
            Debug.LogWarning ($"{nameof (ProjectileBase)}: OwnerPlayerIndex is not set! Please make use of the SpawnProjectile in your script derived from >{nameof(ProjectileWeapon)}<");

        PlayerInput otherPlayer = collision.transform.GetComponent<PlayerInput>();
        if ( otherPlayer != null && otherPlayer.playerIndex != this.OwnerPlayerIndex)
        {
            // Actually subtract damage here
            Debug.Log ($"Did {Damage} Damage to {otherPlayer.gameObject.name}");
        }

        await DelayedDestructionAsync ();
    }

    // Give the opportunity to do something before destruction, like playing an animation or spawning a particle
    private async Task DelayedDestructionAsync()
    {
        await PreDestructionBehaviourAsync ();
        Destroy (this.gameObject);
    }

    /// <summary>
    /// Leave empty if not needed. You'll need 'using System.Threading.Tasks;' otherwise.
    /// Use
    ///     await Task.Delay (delayMilliseconds);
    /// to add a simple delay. use await in an async function to actually wait for a process to finish (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await).
    /// </summary>
    virtual protected async Task PreDestructionBehaviourAsync ()
    {
        await Task.CompletedTask; // This is just to suppress a harmless warning
    }
}
