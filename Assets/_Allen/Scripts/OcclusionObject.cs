using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionObject : MonoBehaviour
{
    Renderer myRend;
    MeshRenderer meshRend;
    public float displayTime;
    public Material highlightMat;

    private Camera cam;
    private Renderer renderer;
    private Plane[] cameraFrustum;
    private Collider collider;

    private void Start()
    {
        myRend = gameObject.GetComponent<Renderer>();
        meshRend = gameObject.GetComponent<MeshRenderer>();

        cam = Camera.main;
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();

        displayTime = -1;
    }

    private void OnBecameInvisible()
    {
        myRend.enabled = false;
    }

    private void ObjectInView()
    {
        var bounds = collider.bounds;
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(cameraFrustum, bounds))
        {
            renderer.enabled = true;
        }
    }

    public void Select() => meshRend.materials[1] = highlightMat;

    public void Deselect() => meshRend.materials[1] = null;

    public void HitOcclude(float time)
    {
        displayTime = time;
        myRend.enabled = true;
    }

}
