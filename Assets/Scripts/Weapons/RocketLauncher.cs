using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class RocketLauncher : DualBarrelWeapon
{
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
        RocketProjectile rocketProjectile = (Instantiate (ProjectilePrefab, parent.position, parent.rotation)).GetComponent<RocketProjectile>();

        if ( transform != null && transform.GetComponent<Driving>())
            rocketProjectile.Target = transform;
    }

    private async Task DelayedSpawnRocketAsync (Transform target, Transform parentToInstantiateUnder)
    {
        await Task.Delay (_projectileOffsetMilliseconds);
        SpawnRocket (target, parentToInstantiateUnder);
    }
}
