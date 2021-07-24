using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RocketProjectile : ProjectileBase
{
    [SerializeField]
    [Range(1f, 100f)]
    private float _turningSpeed = 50f;
    private Vector3 _transformVector = Vector3.forward;
    private Transform _target = null;
    public Transform Target { get => _target; set => _target = value; }

    // Update is called once per frame
    override protected void FrameUpdate()
    {
        this.transform.Translate (_transformVector * Time.deltaTime * ProjectileSpeed);

        if (Target != null)
        {
            // Slowly rotate to target using Slerp
            Quaternion rotationToTarget = Quaternion.LookRotation(Target.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp (transform.rotation, rotationToTarget, _turningSpeed * Time.deltaTime * 10000000f);
        }
    }

    protected override async Task PreDestructionBehaviourAsync ()
    {
        await Task.CompletedTask;
    }
}
