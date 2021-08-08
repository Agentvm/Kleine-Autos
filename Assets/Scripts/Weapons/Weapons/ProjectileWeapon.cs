using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Use this modified base class or its parent class to create Weapon behaviours
/// </summary>
public abstract class ProjectileWeapon : AimableWeapon
{
    // This bulk could rather be moved to a ProjectileWeapon subclass
    // Serialized Fields
    [SerializeField]
    private GameObject _projectilePrefab;

    public GameObject ProjectilePrefab { get => _projectilePrefab; }

    /// <summary>
    /// Use this Instantiation to be sure to set the PlayerIndex!
    /// </summary>
    protected virtual ProjectileBase SpawnProjectile (Vector3 position, Quaternion rotation)
    {
        // Calling with position & rotation avoids setting Muzzle Point as Parent
        ProjectileBase projectile = Instantiate (ProjectilePrefab, position, rotation).GetComponent<ProjectileBase>();

        if ( projectile == null )
            Debug.LogError ("No Projectile Script is attached to the Projectile Prefab on Weapon " + this.transform.name);
        else
            projectile.OwnerPlayerIndex = PlayerInput.playerIndex;

        return projectile;
    }
}
