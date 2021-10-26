using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class RaceManager : MonoBehaviourSingleton<RaceManager>
{
    private static int _playerCount = 0;
    public static int PlayerCount { get => _playerCount; private set => _playerCount = value; }

    private static List<PlayerGameConfig> _playerGameConfigurations = new List<PlayerGameConfig> {};
    public static List<PlayerGameConfig> PlayerGameConfigurations { get => _playerGameConfigurations; private set => _playerGameConfigurations = value; }

    // Events
    public static event Action<List<Transform>> CarsSpawned;

    private void Start ()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged (Scene oldScene, Scene newScene)
    {
        if (newScene.name == "Main")
            InitializeRace ();
    }

    private void InitializeRace ()
    {
        // Find first Racing Track in Scene to get starting positions
        RacingTrack racingTrack = GameObject.FindObjectOfType<RacingTrack> ();

        int playerCount = PlayerGameConfigurations.Count;
        List<Transform> spawnedCars = new List<Transform> ();

        // Spawn cars at start poses
        for (int i = 0; i < playerCount; i++)
        {
            Transform startPose = null;

            // Find another starting pos if there are no more defined
            if (i >= racingTrack.StartPoses.Count)
            {
                startPose.position = racingTrack.StartPoses[racingTrack.StartPoses.Count - 1].position - Vector3.forward * 3 * i;
                startPose.rotation = racingTrack.StartPoses[racingTrack.StartPoses.Count - 1].rotation;
            }
            else
            {
                startPose = racingTrack.StartPoses[i];
            }

            // Spawn Player Car and parent it to the scene
            PlayerGameConfig config = PlayerGameConfigurations[i];
            spawnedCars.Add(
                GameObject.Instantiate (
                    config.PlayerCar,
                    startPose.position,
                    startPose.rotation,
                    null
                ).transform
            );
        }

        CarsSpawned?.Invoke (spawnedCars);
    }

    public static void Reset ()
    {
        PlayerCount = 0;
        PlayerGameConfigurations.Clear ();
    }
}

public struct PlayerGameConfig
{
    public GameObject PlayerCar;
    public AimableWeapon PlayerWeapon;

    public PlayerGameConfig (GameObject playerCar, AimableWeapon playerWeapon)
    {
        PlayerCar = playerCar;
        PlayerWeapon = playerWeapon;
    }
}