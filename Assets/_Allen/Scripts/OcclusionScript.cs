using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionScript : MonoBehaviour
{
    public int rayAmount = 1500, rayDistance = 300;
    public float stayTime = 2;

    public Camera cam;

    public Vector2[] rPoints;

    private void Start()
    {
        //cam = gameObject.GetComponent<Camera>();
        rPoints = new Vector2[rayAmount];

        GetPoints();
    }

    private void Update()
    {
        CastRays();
    }

    public void GetPoints()
    {
        float x = 0, y = 0;
        for (int i = 0; i < rPoints.Length; i++) 
        {
            if (x > 1)
            {
                x = 0;
                y += 1 / Mathf.Sqrt(rayAmount);
            }

            rPoints[i] = new Vector2(x, y);
            x += 1 / Mathf.Sqrt(rayAmount);
        }
    }

    public void CastRays()
    {
        for (int i = 0; i < rayAmount; i++)
        {
            Ray ray;
            RaycastHit hit;
            OcclusionObject ocl;

            ray = cam.ViewportPointToRay(new Vector3(rPoints[i].x, rPoints[i].y, 0));

            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                if (ocl = hit.transform.GetComponent<OcclusionObject>())
                {
                    ocl.HitOcclude(stayTime);
                }
            }
        }
    }

}
