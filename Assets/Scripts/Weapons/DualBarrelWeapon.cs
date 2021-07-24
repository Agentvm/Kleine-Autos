using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DualBarrelWeapon : ProjectileWeapon
{
    [SerializeField]
    [Tooltip("The Point where the projectile will be spawned")]
    private Transform _secondMuzzlePoint;

    public Transform SecondMuzzlePoint { get => _secondMuzzlePoint; }
}
