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
}
