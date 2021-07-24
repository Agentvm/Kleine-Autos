using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlaster : AimableWeapon
{
    protected override void FrameUpdate ( bool fireButtonPressed, Vector2 currentMousePosition )
    {
        if ( fireButtonPressed && ProjectilePrefab != null )
            Instantiate (ProjectilePrefab, MuzzlePoint);
    }
}
