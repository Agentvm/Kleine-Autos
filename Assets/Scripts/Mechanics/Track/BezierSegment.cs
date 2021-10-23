using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierSegment : Segment
{
    /* ----- PUBLIC METHODS ----- */ 
    public override void Build() 
    {
        transform.position = AnchorIn.Position;

        // create new mesh with geometry for straight segment 
        Mesh mesh = new Mesh();

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        for(float i=0.0f; i<=1.01f; i+=0.1f) {
            Vector3 bezierUp = (1-i) * AnchorIn.transform.up + i * AnchorOut.transform.up; 
            float bezierWidthOffset = (1-i) * AnchorIn.TrackWidth + i * AnchorOut.TrackWidth;
            Vector3 vertexOffset = Vector3.Normalize(Vector3.Cross(bezierUp, BezierDerivative(i))) * 0.5f * bezierWidthOffset;

            vertices.Add(BezierPoint(i) + vertexOffset);
            vertices.Add(BezierPoint(i) - vertexOffset);
            uvs.Add(new Vector2(1,0));
            uvs.Add(new Vector2(1,0));
        }

        List<int> triangles = new List<int>();
        for(int i=0; i<10; i++) {
            for(int j=0; j<3; j++) {
                triangles.Add(2*i+j);
            }      
            for(int j=0; j<3; j++) {
                triangles.Add(2*(i+1)+1-j);
            }      
        }
       
        mesh.vertices = vertices.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.triangles =  triangles.ToArray();

        // apply mesh to MeshFilter 
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    public void OnDrawGizmos() {
        #if UNITY_EDITOR
            Gizmos.color = Color.green;
            for(float i=1; i<=10; i+=1) {
                Gizmos.DrawLine(AnchorIn.transform.position +  BezierPoint((i-1)*0.1f), AnchorIn.transform.position + BezierPoint(i*0.1f));
            }   
        #endif 
    }

    private Vector3 BezierPoint(float time) {
        Vector3 p0 = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 p3 = AnchorOut.Position - AnchorIn.Position;
        Vector3 p1 = p0 +  OrientationOffsetIn * AnchorIn.Orientation * Vector3.forward * ((p3-p0).magnitude / 2.0f);   
        Vector3 p2 = p3 - OrientationOffsetOut * AnchorOut.Orientation * Vector3.forward * ((p3-p0).magnitude / 2.0f);  
       
        // calculate point on cubic bezier curve based on time
        return Mathf.Pow(1-time, 3.0f) * p0 + 3.0f * Mathf.Pow(1-time, 2.0f) * time * p1 + 3.0f * (1-time) * Mathf.Pow(time, 2.0f) * p2 + Mathf.Pow(time, 3.0f) * p3;
    }

    private Vector3 BezierDerivative(float time) {
         // 1. points = anchor in, 2. point = anchor in direction + unit, 3. point = anchor out - unit, 4. point = anchor out 
        Vector3 p0 = AnchorIn.Position;
        Vector3 p3 = AnchorOut.Position;
        Vector3 p1 = p0 +  OrientationOffsetIn * AnchorIn.Orientation * Vector3.forward * ((p3-p0).magnitude / 2);   
        Vector3 p2 = p3 - OrientationOffsetOut * AnchorOut.Orientation * Vector3.forward * ((p3-p0).magnitude / 2);  

        return 3.0f * Mathf.Pow(1-time, 2.0f) * (p1-p0) + 6.0f * (1-time) * time * (p2-p1) + 3.0f * Mathf.Pow(time, 2.0f) * (p3-p2);
    }
}
