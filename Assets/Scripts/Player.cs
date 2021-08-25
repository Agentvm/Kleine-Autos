using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float BestLapTime{ get; private set; } = Mathf.Infinity;
    public float LastLapTime{ get; private set; } = 0;
    public float CurrentLapTime{ get; private set; } = 0;
    public int CurrentLap{ get; private set; } = 0;
    private float lapTimerTimestamp;
    private int lastCheckpointPassed = 0;

    private Transform checkpointsParent;
    private int checkpointCount;
    private int checkpointLayer;

    // Start is called before the first frame update
    void Awake() 
    {
        checkpointsParent = GameObject.Find("Checkpoints").transform;
        checkpointCount = checkpointsParent.childCount;
        checkpointLayer = LayerMask.NameToLayer("Checkpoint");
    }

    void StartLap()
    {
        Debug.Log("Starting Lap");
        CurrentLap++;
        lastCheckpointPassed = 1;
        lapTimerTimestamp = Time.time;
    }

    void EndLap()
    {
        LastLapTime = Time.time - lapTimerTimestamp;
        BestLapTime = Mathf.Min(LastLapTime, BestLapTime);
        Debug.Log("Ending Lap - Lap Time: " + LastLapTime + " seconds");
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.layer != checkpointLayer)
        {
            return;
        }

        // Checkpoint 1
        if(collider.gameObject.name == "Checkpoint1")
        {
            if(lastCheckpointPassed == checkpointCount)
            {
                EndLap();
            }

            if(CurrentLap == 0 || lastCheckpointPassed == checkpointCount)
            {
                StartLap();
            }

            return;
        }

        // Other Checkpoints
        if(collider.gameObject.name == "Checkpoint" + (lastCheckpointPassed+1).ToString())
        {
            lastCheckpointPassed++;
            Debug.Log("Checkpoint " + lastCheckpointPassed + " reached");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CurrentLapTime = lapTimerTimestamp > 0 ? Time.time - lapTimerTimestamp : 0;
    }
}
