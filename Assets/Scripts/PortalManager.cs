using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using Debug = UnityEngine.Debug;

public class PortalManager : MonoBehaviour
{
    public GameObject MainCamera;
    
    public Material Walls;
    public Material Chichen_Itza;
    public Material Sphinx;
    public Material Petra;
    private List<Material> Materials;
    private static readonly int StencilComp = Shader.PropertyToID("_StencilComp");

    private bool triggerActive = false;

    void Start()
    {
        
         Materials = GetComponent<Renderer>().materials.ToList();
         Debug.Log(Materials);
    }

    private void Update()
    {
        if (!triggerActive)
        {
            Walls.SetInt(StencilComp, (int)CompareFunction.Equal);
            Chichen_Itza.SetInt(StencilComp, (int)CompareFunction.Equal);
            Sphinx.SetInt(StencilComp, (int)CompareFunction.Equal);
            Petra.SetInt(StencilComp, (int)CompareFunction.Equal);
        }
    }

    void OnTriggerStay (Collider collider)
    {
        triggerActive = true;
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(MainCamera.transform.position);

        if (camPositionInPortalSpace.y < 0.5f)
        {
            Walls.SetInt(StencilComp, (int)CompareFunction.Always);
            Chichen_Itza.SetInt(StencilComp, (int)CompareFunction.Always);
            Sphinx.SetInt(StencilComp, (int)CompareFunction.Always);
            Petra.SetInt(StencilComp, (int)CompareFunction.Always);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        triggerActive = false;
    }
}
