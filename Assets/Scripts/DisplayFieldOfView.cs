using System.Collections.Generic;
using UnityEngine;

/**
 * Clase para el renderizado del cono de vision de los personajes
 */

public class DisplayFieldOfView : MonoBehaviour
{
    public bool show = true;
    [Range(0, 360)]
    public float viewAngle;
    public float viewRadious;
    public float meshResolution;
    public LayerMask obstacleMask;

    public MeshFilter meshFilter;
    Mesh viewMesh;

    // Start is called before the first frame update
    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        meshFilter.mesh = viewMesh;
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            DrawFieldOfView();
        }
    }

    void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngle = viewAngle / stepCount;

        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle/2 + stepAngle * i;
            ViewCastInfo newViewCast = viewCast(angle);
            points.Add(newViewCast.point);
        }

        int vertexCount = points.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];
        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertexCount-1; i++)
        {
            vertices[i+1] = transform.InverseTransformPoint(points[i]);
            if(i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();
    }

    ViewCastInfo viewCast (float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dir, out hit, viewRadious, obstacleMask))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * viewRadious, viewRadious, globalAngle);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float dst;
        public float angle;

        public ViewCastInfo(bool hit, Vector3 point, float dst, float angle)
        {
            this.hit = hit;
            this.point = point;
            this.dst = dst;
            this.angle = angle;
        }
    }

    internal void SetShow(bool b)
    {
        show = b;
        meshFilter.GetComponent<MeshRenderer>().enabled = b;
    }
}
