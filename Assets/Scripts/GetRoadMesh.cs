using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class GetRoadMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh roadMesh = this.GetComponent<MeshFilter>().mesh;
        this.GetComponent<MeshCollider>().sharedMesh = roadMesh;
    }
}
