using System;
using UnityEngine;
using UnityEngine.InputSystem;
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

    // Properties
    InputDevice _inputDevice = null;
    public InputDevice InputDevice { get => _inputDevice; set => _inputDevice = value; }

    // Events
    public static event Action MenuPanelCountChanged;

    public void ShowGamepadIcon(bool showGamepad = true)
    {
        _iconGamepad.gameObject.SetActive(showGamepad);
        _iconKeyboard.gameObject.SetActive(!showGamepad);
    }

    public void RegisterPlayerConfig()
    {
        Debug.Log("Player Config Added");
        RaceManager.PlayerGameConfigurations.Add(new PlayerGameConfig(_playerCar, _playerWeapon));
    }

    private void OnEnable()
    {
        OnMenuPanelCountChanged();
    }

    private void OnDestroy()
    {
        OnMenuPanelCountChanged();
    }

    void OnMenuPanelCountChanged()
    {
        MenuPanelCountChanged?.Invoke();
    }
}
