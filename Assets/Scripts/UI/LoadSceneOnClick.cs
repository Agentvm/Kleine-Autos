using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LoadSceneOnClick : MonoBehaviour
{
    [SerializeField] private SceneAsset _scene = null;
    private Button _button = null;
    private string _scenePath = "";

    // Events
    public static event Action RaceStarted;

    private void Awake ()
    {
        _scenePath = AssetDatabase.GetAssetPath (_scene);
        this._button = this.GetComponent<Button> ();
        this._button.onClick.AddListener (OnClick);
    }

    private void OnClick ()
    {
        OnRaceStarted ();
        SceneManager.LoadScene (_scenePath);
    }

    private void OnRaceStarted()
    {
        RaceStarted?.Invoke ();
    }
}
