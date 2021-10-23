using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tooltip Missing")]
    private Vector3 _cameraOffset = default;
    #region CachedProperties
    private GameObject _followingCamera;

    public GameObject FollowingCameraTemplate
    {
        get
        {
            if (_followingCamera == null)
                _followingCamera = this.transform.Find(nameof(FollowingCameraTemplate)).gameObject;

            return _followingCamera;
        }
    }

    #endregion

    private void Awake ()
    {
        RaceManager.CarsSpawned += Initialize;
    }

    // Start is called before the first frame update
    public void Initialize (List<Transform> spawnedCars)
    {
        for (int i = 0; i < spawnedCars.Count; i++)
        {
            FollowingCamera camera = GameObject.Instantiate (
                        FollowingCameraTemplate,
                        spawnedCars[i].position + _cameraOffset,
                        new Quaternion (),
                        this.transform).GetComponent<FollowingCamera>();
            camera.FollowedGameObject = spawnedCars[i].gameObject;
            camera.gameObject.SetActive (true);
            camera.transform.LookAt (spawnedCars[i].transform);
        }

    }
}
