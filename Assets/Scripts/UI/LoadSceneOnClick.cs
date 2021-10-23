using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    [SerializeField] private SceneAsset _scene = null;
    private Button _button = null;
    private string _scenePath = "";

    private void Awake ()
    {
        _scenePath = AssetDatabase.GetAssetPath (_scene);
        this._button = this.GetComponent<Button> ();
        this._button.onClick.AddListener (OnClick);
    }

    private void OnClick ()
    {
        SceneManager.LoadScene (_scenePath);
    }
}
