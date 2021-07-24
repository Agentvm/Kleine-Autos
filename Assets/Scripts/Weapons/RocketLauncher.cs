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
            SpawnRocket (MuzzlePoint, hitInfo.transform);
            await DelayedSpawnRocketAsync (SecondMuzzlePoint, hitInfo.transform);
        }
    }

    private void SpawnRocket(Transform spawnPose, Transform target )
    {
        RocketProjectile rocketProjectile = SpawnProjectile (spawnPose.position, spawnPose.rotation) as RocketProjectile;

        if ( target != null && (target.GetComponent<UnityEngine.InputSystem.PlayerInput>() || target.GetComponent<AimableWeapon> ()) )
        {
            Debug.Log ("Setting Rocket Target to: " + target.name);
            rocketProjectile.Target = target;
        }
    }

    private async Task DelayedSpawnRocketAsync (Transform spawnPose, Transform target)
    {
        await Task.Delay (_projectileOffsetMilliseconds);
        SpawnRocket (spawnPose, target);
    }
}
