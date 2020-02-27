﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private Mesh mesh;

    private float fov = 90f, viewDistance = 5f, angleIncrease, startAngle = 0;

    private int rayCount = 70 ;

    private Vector3 origin_ = Vector3.zero;

    private Vector3[] vertices;
  //  private Vector2[] uv;

    private int[] triangles;

    [SerializeField]
    private LayerMask mask;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        angleIncrease = fov / rayCount;
        vertices = new Vector3[rayCount + 2];
       // uv = new Vector2[vertices.Length];
        triangles = new int[rayCount * 3];
    }

    private void LateUpdate()
    {
        float angle = startAngle;
        int verIndex = 1;
        int triaIndex = 0;

        vertices[0] = origin_;

        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 ver = origin_ + Vec3FromAngle(angle) * viewDistance;
            RaycastHit2D hit = Physics2D.Raycast(origin_, Vec3FromAngle(angle), viewDistance , mask);

            if (hit.collider == null)
            {
                ver = origin_ + Vec3FromAngle(angle) * viewDistance;
            }
            else
            {
                ver = hit.point;
            }
            vertices[verIndex] = ver;

            if (i > 0)
            {
                triangles[triaIndex] = 0;
                triangles[triaIndex + 1] = verIndex - 1;
                triangles[triaIndex + 2] = verIndex;
                triaIndex += 3;
            }
            verIndex++;
            angle -= angleIncrease;
        }


        mesh.vertices = vertices;
        //mesh.uv = uv;
        mesh.triangles = triangles;
    }

    private Vector3 Vec3FromAngle(float angle)
    {
        float newAngle = angle * (Mathf.PI / 180);
        return new Vector3(Mathf.Cos(newAngle), Mathf.Sin(newAngle));
    }

    private float AngleFromVec3(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (n < 0) n += 360;
        return n;
    }

    public void SetOrigin(Vector3 origin)
    {
        origin_ = origin;
    }

    public void SetAngle(Vector3 aimDir)
    {
        startAngle = AngleFromVec3(aimDir) - fov / 2f;
    }

    public void SetFov(float fov)
    {
        this.fov = fov;
        angleIncrease = fov / rayCount;
    }

    public void SetLimit(float limit)
    {
        viewDistance = limit;
    }
}