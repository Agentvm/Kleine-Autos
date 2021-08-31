using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleStatus : MonoBehaviour
{
    [SerializeField]
    [Range (10, 100)]
    [Tooltip("Hitpoints")]
    private int _maximumIntegrity = 40;

    // Variables
    private int currentIntegrity = 0;

    public int CurrentIntegrity { get => currentIntegrity; private set => currentIntegrity = value; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentIntegrity = _maximumIntegrity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamageVehicle(int damage)
    {
        Debug.Log ($"Did {damage} Damage to {this.gameObject.name}");
    }
}
