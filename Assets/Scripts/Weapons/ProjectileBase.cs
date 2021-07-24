using System.Collections;
using System.Collections.Generic;
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

    // Properties
    public float ProjectileSpeed { get => _projectileSpeed; private set => _projectileSpeed = value; }
    public int Damage { get => _damage; private set => _damage = value; }
    public float DestructionTime { get => _destructionTime; private set => _destructionTime = value; }

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

    // Triggered by the attached Collider Component
    private async void OnCollisionEnter ( Collision collision )
    {
        Driving otherPlayer = collision.transform.GetComponent<Driving>();
        if ( otherPlayer )
            Debug.Log ($"Did {Damage} Damage");

        await DelayedDestructionAsync ();
    }

    // Give the opportunity to do something before destruction, like playing an animation or spawning a particle
    private async Task DelayedDestructionAsync()
    {
        await PreDestructionBehaviourAsync ();
        Destroy (this.gameObject);
    }

    /// <summary>
    /// Leave empty if not needed.
    /// Use
    ///     await Task.Delay (delayMilliseconds);
    /// to add a simple delay. use await in an async function to actually wait for a process to finish (https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/await).
    /// </summary>
    virtual protected async Task PreDestructionBehaviourAsync ()
    {
        await Task.CompletedTask; // This is just to suppress a harmless warning
    }
}
