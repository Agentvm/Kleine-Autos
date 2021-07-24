using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : ProjectileBase
{
    private Vector3 _transformVector = Vector3.forward;

    protected override void FrameUpdate ()
    {
        this.transform.Translate (_transformVector * Time.deltaTime * ProjectileSpeed);
    }
}
