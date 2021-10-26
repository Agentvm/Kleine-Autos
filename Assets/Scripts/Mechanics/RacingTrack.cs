using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingTrack : MonoBehaviour
{
    [SerializeField]
    List<Transform> _startingPositions = new List<Transform> ();

    public List<Transform> StartPoses { get => _startingPositions;}
}
