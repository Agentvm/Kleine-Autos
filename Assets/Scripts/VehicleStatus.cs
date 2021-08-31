using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public enum VehicleDamageState
{
    None,
    Light,
    Medium,
    Severe
}


[RequireComponent (typeof (Driving))]
public class VehicleStatus : MonoBehaviour
{
    [SerializeField]
    [Range (10, 100)]
    [Tooltip ("Hitpoints")]
    private int _maximumIntegrity = 40;

    // Variables
    private int _currentIntegrity = 0;
    private bool _currentlyDestroyed = false;

    // Properties
    public VehicleDamageState CurrentDamageState
    {
        get
        {
            float damagePercentage = _maximumIntegrity / _currentIntegrity;

            if (damagePercentage < 0.2f)
                return VehicleDamageState.Severe;
            else if (damagePercentage < 0.5f)
                return VehicleDamageState.Medium;
            else if (CurrentIntegrity < _maximumIntegrity)
                return VehicleDamageState.Light;

            return VehicleDamageState.None;
        }
    }
    public int CurrentIntegrity { get => _currentIntegrity; private set => _currentIntegrity = value; }
    public bool CurrentlyDestroyed { get => _currentlyDestroyed; private set => _currentlyDestroyed = value; }

    // Cached Properties
    #region CachedProperties
    private Driving _drivingScript;

    public Driving DrivingScript
    {
        get
        {
            if (_drivingScript == null)
                _drivingScript = this.GetComponent<Driving> ();

            return _drivingScript;
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start ()
    {
        CurrentIntegrity = _maximumIntegrity;
    }

    public async void DamageVehicle (int damage)
    {
        Debug.Log ($"Received {damage} Damage to {this.gameObject.name}");
        this._currentIntegrity -= damage;

        if (this._currentIntegrity <= 0)
        {
            await this.Destroy ();
        }
    }

    private async Task Destroy ()
    {
        this._currentlyDestroyed = true;

        Debug.LogWarning ($"Vehicle {this.gameObject.name} was destroyed.");
        this.gameObject.SetActive (false);
        DrivingScript.SteeringDisabled = true;
        await this.Respawn ();
    }

    private async Task Respawn ()
    {
        await Task.Delay (1500);
        this._currentIntegrity = _maximumIntegrity;
        DrivingScript.SteeringDisabled = false;
        this.gameObject.SetActive (true);
    }
}
