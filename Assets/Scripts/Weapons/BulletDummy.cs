using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDummy : MonoBehaviour
{
    private Vector3 _transformVector = Vector3.forward;
    [SerializeField][Range(1f, 10f)][Tooltip("Time until projectile disappears")]
    private float _destructionTime = 6f;
    private float _currentTime = 0f;

    private void Start ()
    {
        _currentTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate (_transformVector * Time.deltaTime * 50f);

        if ( Time.time > _currentTime + _destructionTime )
            Destroy (this.transform.gameObject);
    }
}
