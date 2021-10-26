using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerCar = null;
    [SerializeField]
    private AimableWeapon _playerWeapon = null;
    [SerializeField]
    int _panelNumber = 0;

    public int PanelNumber { get => _panelNumber; private set => _panelNumber = value; }

    // Start is called before the first frame update
    void Start()
    {
        RaceManager.Reset ();
        LoadSceneOnClick.RaceStarted += AddPlayerConfig;
    }

    private void AddPlayerConfig ()
    {
        RaceManager.PlayerGameConfigurations.Add (new PlayerGameConfig (_playerCar, _playerWeapon));
    }
}
