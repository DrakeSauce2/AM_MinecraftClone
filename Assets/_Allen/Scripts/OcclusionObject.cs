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

    private bool isHighlighted = false;

    private void OnEnable()
    {
        myRend = gameObject.GetComponent<Renderer>();
        meshRend = gameObject.GetComponent<MeshRenderer>();
        displayTime = -1;
    }

    private void Update()
    {
        if (displayTime > 0)
        {
            displayTime -= Time.deltaTime;
            myRend.enabled = true;
        } else {
            myRend.enabled = false;
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
