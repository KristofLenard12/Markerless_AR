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

    private bool triggerActive;

    void Start()
    {
        
        Walls.SetInt(StencilComp, (int)CompareFunction.Equal);
        Chichen_Itza.SetInt(StencilComp, (int)CompareFunction.Equal);
        Sphinx.SetInt(StencilComp, (int)CompareFunction.Equal);
        Petra.SetInt(StencilComp, (int)CompareFunction.Equal);
    }

    private void Update()
    {
        
    }

    void OnTriggerStay (Collider collider)
    {
        
        Vector3 camPositionInPortalSpace = transform.InverseTransformPoint(MainCamera.transform.position);

        if (camPositionInPortalSpace.x < 0f)
        {
            triggerActive = true;
            Walls.SetInt(StencilComp, (int)CompareFunction.Always);
            Chichen_Itza.SetInt(StencilComp, (int)CompareFunction.Always);
            Sphinx.SetInt(StencilComp, (int)CompareFunction.Always);
            Petra.SetInt(StencilComp, (int)CompareFunction.Always);
        }
        else
        {
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        Walls.SetInt(StencilComp, (int)CompareFunction.Equal);
        Chichen_Itza.SetInt(StencilComp, (int)CompareFunction.Equal);
        Sphinx.SetInt(StencilComp, (int)CompareFunction.Equal);
        Petra.SetInt(StencilComp, (int)CompareFunction.Equal);
    }
}
