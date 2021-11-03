using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] RectTransform _playerGridLayout = null;
    [SerializeField] GameObject _playerPanelPrefab = null;
    [SerializeField] private SceneAsset _scene = null;
    [SerializeField] private Button _startButton = null;

    // Variables
    private string _scenePath = "";
    private List<MenuPanelPlayer> _playerPanels = new List<MenuPanelPlayer>();

    private void Awake()
    {
        _scenePath = AssetDatabase.GetAssetPath(_scene);
        this._startButton.onClick.AddListener(OnClick);
    }

    private void OnEnable()
    {
        RaceManager.Reset();
    }

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

        // Listen for Devices dis- or reconnecting
        InputSystem.onDeviceChange += InputSystem_onDeviceChange;
    }

    void CreateMenuPanel(InputDevice inputDevice)
    {
        MenuPanelPlayer menuPanelPlayer = Instantiate(_playerPanelPrefab, _playerGridLayout).GetComponent<MenuPanelPlayer>();
        menuPanelPlayer.ShowGamepadIcon(inputDevice is Gamepad);
        menuPanelPlayer.InputDevice = inputDevice;
        _playerPanels.Add(menuPanelPlayer);
    }

    void RemoveMenuPanel(InputDevice inputDevice)
    {
        MenuPanelPlayer panelToRemove = _playerPanels.FirstOrDefault(panel => panel.InputDevice == inputDevice);
        if (panelToRemove != default)
        {
            Destroy(panelToRemove.gameObject);
            _playerPanels.Remove(panelToRemove);
        }
    }

    private void InputSystem_onDeviceChange(InputDevice device, InputDeviceChange modeChange)
    {
        switch (modeChange)
        {
            case InputDeviceChange.Added:
                CreateMenuPanel(device);
                break;
            case InputDeviceChange.Disconnected:
                RemoveMenuPanel(device);
                break;
            case InputDeviceChange.Reconnected:
                // Somehow, this is triggered in Addition to InputDeviceChange.Added; therefore disabled
                //CreateMenuPanel(device);
                break;
            case InputDeviceChange.Removed:
                // Remove from Input System entirely; by default, Devices stay in the system once discovered.
                break;
            default:
                // See InputDeviceChange reference for other event types.
                break;
        }
    }

    private void OnClick()
    {
        foreach (MenuPanelPlayer panel in _playerPanels)
            panel.RegisterPlayerConfig();
        SceneManager.LoadScene(_scenePath);
    }
}
