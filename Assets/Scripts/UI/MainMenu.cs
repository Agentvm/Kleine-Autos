using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] RectTransform _playerGridLayout = null;
    [SerializeField] GameObject _playerPanelPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        // Discover keyboards and Gamepads
        IEnumerable<InputDevice> gamepads = InputSystem.devices.Where(device => device is Gamepad);
        foreach (InputDevice gamepad in gamepads)
        {
            Debug.Log(gamepad);
            CreateMenuPanel(gamepad);
        }

        IEnumerable<InputDevice> keyboards = InputSystem.devices.Where(device => device is Keyboard);
        foreach (InputDevice keyboard in keyboards)
        {
            Debug.Log(keyboard);
            CreateMenuPanel(keyboard);
        }

        InputSystem.onDeviceChange += InputSystem_onDeviceChange;
    }

    void CreateMenuPanel (InputDevice inputDevice)
    {
        MenuPanelPlayer menuPanelPlayer = Instantiate(_playerPanelPrefab, _playerGridLayout).GetComponent<MenuPanelPlayer>();
        menuPanelPlayer.ShowGamepadIcon(inputDevice is Gamepad);
    }

    private void InputSystem_onDeviceChange(InputDevice device, InputDeviceChange modeChange)
    {
        switch (modeChange)
        {
            case InputDeviceChange.Added:
                // New Device.
                break;
            case InputDeviceChange.Disconnected:
                // Device got unplugged.
                break;
            case InputDeviceChange.Reconnected:
                // Plugged back in.
                break;
            case InputDeviceChange.Removed:
                // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                break;
            default:
                // See InputDeviceChange reference for other event types.
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
