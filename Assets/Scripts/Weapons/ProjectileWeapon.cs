using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeapon : AimableWeapon
{
    // This bulk could rather be moved to a ProjectileWeapon subclass
    // Serialized Fields
    [SerializeField]
    private GameObject _projectilePrefab;

    public GameObject ProjectilePrefab { get => _projectilePrefab; }
}
