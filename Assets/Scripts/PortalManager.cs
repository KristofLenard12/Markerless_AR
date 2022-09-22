using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PortalManager : MonoBehaviour
{
    public GameObject MainCamera;

    public GameObject Sponza;

    private Material[] SponzaMaterials;
    private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");

    private bool triggerActive = false;

    void Start()
    {
        SponzaMaterials = Sponza.GetComponent<Renderer>().sharedMaterials;
        Debug.Log(SponzaMaterials[0]);
    }

    private void Update()
    {
        if (!triggerActive)
        {
            for (int i = 0; i < SponzaMaterials.Length; i++)
            {
                SponzaMaterials[i].SetInt(StencilComp, (int)CompareFunction.Equal);
            }
        }
    }

    void OnTriggerStay (Collider collider)
    {
        triggerActive = true;
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(MainCamera.transform.position);

        if (camPositionInPortalSpace.y < 0.5f)
        {
            Debug.Log("here");
            for (int i = 0; i < SponzaMaterials.Length; i++)
            {
                SponzaMaterials[i].SetInt(StencilComp, (int)CompareFunction.Always);
            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        triggerActive = false;
    }
}
