using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private FieldOfView fov;
    [SerializeField]
    private float fovSet, limit;
    [SerializeField]
    private Material fovMat;

    void Start()
    {
        fov = Instantiate(fov.gameObject, Vector3.zero, Quaternion.identity).GetComponent<FieldOfView>();
        fov.name = "FieldOfView" + this.name;
        fov.SetInstance(limit, fovSet, false);
        fov.gameObject.layer = this.gameObject.layer;
        fov.setMaterial(fovMat);
    }

    private void Update()
    {
        if (fov != null)
        {
            if (fovSet != 360)
            {
                fov.SetAngle(-transform.right);
            }
            fov.SetOrigin(transform.position);
        }
    }
}
