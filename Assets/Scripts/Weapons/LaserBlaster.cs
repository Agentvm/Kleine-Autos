using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlaster : ProjectileWeapon
{
    protected override void FrameUpdate ( bool fireButtonPressed, RaycastHit hitInfo )
    {
        if ( fireButtonPressed && ProjectilePrefab != null )
            Instantiate (ProjectilePrefab, MuzzlePoint);
    }
}
