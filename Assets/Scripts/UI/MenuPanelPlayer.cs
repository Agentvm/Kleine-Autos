using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelPlayer : MonoBehaviour
{
    // Serialized Fields
    [SerializeField]
    private GameObject _playerCar = null;
    [SerializeField]
    private AimableWeapon _playerWeapon = null;
    [SerializeField]
    private Image _iconGamepad = null;
    [SerializeField]
    private Image _iconKeyboard = null;

    // Variables
    int _playerNumber = 0;
    
    // Properties
    public int PlayerNumber { get => _playerNumber; set => _playerNumber = value; }

    // Start is called before the first frame update
    void Start()
    {
        MainMenu.RaceStarted += AddPlayerConfig;
    }

    public void ShowGamepadIcon (bool showGamepad = true)
    {
        _iconGamepad.gameObject.SetActive(showGamepad);
        _iconKeyboard.gameObject.SetActive(!showGamepad);
    }

    private void AddPlayerConfig ()
    {
        Debug.Log("Player Config Added");
        RaceManager.PlayerGameConfigurations.Add (new PlayerGameConfig (_playerCar, _playerWeapon));
    }
}
