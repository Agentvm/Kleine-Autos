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

    private void Start ()
    {
        _currentTime = Time.time;
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

    // Triggered by the attached Collider Component (which has to be set to 'isTrigger', while having a Rigidbody Component which is kinematic)
    private async void OnTriggerEnter ( Collider collision )
    {
        // Return if colliding with Projectile
        if ( collision.transform.GetComponent<ProjectileBase> () )
            return;

        // Check for collision with other player
        if ( this.OwnerPlayerIndex == null )
            Debug.LogWarning ($"{nameof (ProjectileBase)}: OwnerPlayerIndex is not set!" +
                $"Please make use of the SpawnProjectile() function in your script derived from >{nameof(ProjectileWeapon)}<");

        // Try to get PlayerInput Script to determine if the object hit was a player
        // Look on parent transform if the script is not found (because the mesh collider may not be on the same object as the car scripts)
        PlayerInput otherPlayer = collision.transform.GetComponent<PlayerInput>();
        if ( otherPlayer == null && collision.transform.parent != null)
            otherPlayer = collision.transform.parent.GetComponent<PlayerInput> ();

        // Check if the PlayerInput index is different from ours (see SpawnProjectile() function in ProjectileWeapon.cs")
        if ( otherPlayer != null && otherPlayer.playerIndex != this.OwnerPlayerIndex )
        {
            // Actually subtract damage here
            Debug.Log ($"Did {Damage} Damage to {otherPlayer.gameObject.name}");

            await DelayedDestructionAsync ();
            return;
        }
        
        // Colliding with own weapon or car, don't destroy
        if ( collision.transform.GetComponent<AimableWeapon> () || collision.transform.GetComponent<PlayerInput>())
            return;

        // Destroy on Collision after a delay which has to be specified by subclass
        await DelayedDestructionAsync ();
    }

    // Give the opportunity to do something before destruction, like playing an animation or spawning a particle
    private async Task DelayedDestructionAsync()
    {
        // This will be filled in by the derived classes
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
