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
            Instantiate (ProjectilePrefab, MuzzlePoint);
            await DelayedSpawnAsync ();
        }
    }

    private async Task DelayedSpawnAsync ()
    {
        await Task.Delay (_projectileOffsetMilliseconds);
        Instantiate (ProjectilePrefab, SecondMuzzlePoint);
    }
}
