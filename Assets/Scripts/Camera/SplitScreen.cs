using System.Collections.Generic;
using UnityEngine;

public class SplitScreen : MonoBehaviour
{
    [SerializeField]
    [Tooltip ("Tooltip Missing")]
    private Vector3 _cameraOffset = default;
    #region CachedProperties
    private GameObject _followingCamera;

    public GameObject FollowingCameraTemplate
    {
        get
        {
            if (_followingCamera == null)
                _followingCamera = this.transform.Find (nameof (FollowingCameraTemplate)).gameObject;

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
        // Spawn Cameras
        int playerCount = spawnedCars.Count;
        List<Camera> cameras = new List<Camera> ();
        for (int playerIndex = 0; playerIndex < playerCount; playerIndex++)
        {
            // Instantiate Cameras with the correct offset
            FollowingCamera camera = GameObject.Instantiate (
                        FollowingCameraTemplate,
                        spawnedCars[playerIndex].position + _cameraOffset,
                        new Quaternion (),
                        this.transform).GetComponent<FollowingCamera> ();

            // Attach Cameras to cars
            camera.FollowedGameObject = spawnedCars[playerIndex].gameObject;
            camera.transform.LookAt (spawnedCars[playerIndex].transform);
            camera.gameObject.SetActive (true);
            cameras.Add (camera.GetComponent<Camera> ());
        }

        // Split Screen
        Debug.Log ($"Mathf.CeilToInt ({cameras.Count} / {2}) = {Mathf.CeilToInt ((float)cameras.Count / 2f)}");
        int splits = Mathf.CeilToInt ((float)cameras.Count / 2f);
        if (cameras.Count == 1)
            splits = 0;

        float screenWidth = 1;
        float screenHeight = 1;

        if (splits > 0)
        {
            screenWidth = 1f / splits;
            screenHeight = 1f / splits;
        }
        if (splits % 2 == 1)
        {
            screenWidth = 1f / (splits + 1);
            screenHeight = 1f / splits;
        }

        Debug.Log ("splits: " + splits);
        Debug.Log ("cameras.Count: " + cameras.Count);

        for (int xIndex = 0; xIndex < splits; xIndex++)
        {
           for (int yIndex = 0; yIndex < Mathf.Max(splits, 2); yIndex++)
            {
                if (xIndex + yIndex >= cameras.Count)
                    continue;

                Camera camera = cameras[xIndex + yIndex];
                float xPosition = screenWidth * yIndex;
                float yPosition = screenHeight * xIndex;
                camera.rect = new Rect (xPosition, yPosition, screenWidth, screenHeight);
                Debug.Log ($"new Rect ({xPosition}, {yPosition}, {screenWidth}, {screenHeight});");
            }
        }
    }
}
