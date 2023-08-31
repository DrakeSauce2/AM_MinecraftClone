using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionScript : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam); 
        foreach (Plane plane in planes)
        {
            //Instantiate(plane, Vector3.zero, Quaternion.identity);
        }
    }

    private void Update()
    {
        
    }

    

}
