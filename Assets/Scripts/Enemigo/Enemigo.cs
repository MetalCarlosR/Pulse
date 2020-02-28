using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private FieldOfView fov  = null;
    [SerializeField]
    private float fovSet = 90f, limit = 5f;
    [SerializeField]
    private Material fovMat = null;

    void Start()
    {
        fov = Instantiate(fov.gameObject , null).GetComponent<FieldOfView>();
        fov.name = "FieldOfView" + this.name;
        fov.SetInstance(limit, fovSet);
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

    private void OnDestroy()
    {
        if (fov != null)
        {
            Destroy(fov.gameObject);
        }
    }
}
