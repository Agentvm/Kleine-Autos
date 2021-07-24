using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class RocketLauncher : ProjectileWeapon
{
    [SerializeField]
    [Tooltip("The Point where the second projectile will be spawned")]
    private Transform _secondMuzzlePoint;

    public Transform SecondMuzzlePoint { get => _secondMuzzlePoint; }
    [SerializeField]
    [Range(0,1000)]
    private int _projectileOffsetMilliseconds = 300;

    protected override async void FrameUpdate ( bool fireButtonPressed, RaycastHit hitInfo )
    {
        if ( fireButtonPressed && ProjectilePrefab != null )
        {
            SpawnRocket (hitInfo.transform, MuzzlePoint);
            await DelayedSpawnRocketAsync (hitInfo.transform, SecondMuzzlePoint);
        }
    }

    private void SpawnRocket(Transform target, Transform parent)
    {
        RocketProjectile rocketProjectile = SpawnProjectile (MuzzlePoint.position, MuzzlePoint.rotation) as RocketProjectile;

        if ( transform != null && transform.GetComponent<Driving>())
            rocketProjectile.Target = transform;
    }

    private async Task DelayedSpawnRocketAsync (Transform target, Transform parentToInstantiateUnder)
    {
        await Task.Delay (_projectileOffsetMilliseconds);
        SpawnRocket (target, parentToInstantiateUnder);
    }
}
