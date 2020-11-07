﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonCameraMovement : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;

    public float topRigMin = 12.0f;
    public float topRigMax = 15.0f;
    public float midRigMin = 20.0f;
    public float midRigMax = 25.0f;
    public float botRigMin = 12.0f;
    public float botRigMax = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Old camera movement
        // Controlls where player is looking only when right-click is held
        /* 
        if (Input.GetMouseButtonDown(1))
        {
            freeLookCam.m_XAxis.m_InputAxisName = "Mouse X";
        }
        if (Input.GetMouseButtonUp(1))
        {
            freeLookCam.m_XAxis.m_InputAxisName = "";
            freeLookCam.m_XAxis.m_InputAxisValue = 0;
        }
        if (Input.GetMouseButtonDown(1))
        {
            freeLookCam.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        if (Input.GetMouseButtonUp(1))
        {
            freeLookCam.m_YAxis.m_InputAxisName = "";
            freeLookCam.m_YAxis.m_InputAxisValue = 0;
        }
        */

        if (Input.mouseScrollDelta.y < 0)
        {
            
            float topRad = freeLookCam.m_Orbits[0].m_Radius + 0.6f;
            freeLookCam.m_Orbits[0].m_Radius = Mathf.Clamp(topRad, topRigMin, topRigMax);

            float midRad = freeLookCam.m_Orbits[1].m_Radius + 1.0f;
            freeLookCam.m_Orbits[1].m_Radius = Mathf.Clamp(midRad, midRigMin, midRigMax);

            float botRad = freeLookCam.m_Orbits[2].m_Radius + 0.6f;
            freeLookCam.m_Orbits[2].m_Radius = Mathf.Clamp(botRad, topRigMin, topRigMax);
        }
        else if (Input.mouseScrollDelta.y > 0)
        {
            float topRad = freeLookCam.m_Orbits[0].m_Radius - 0.6f;
            freeLookCam.m_Orbits[0].m_Radius = Mathf.Clamp(topRad, topRigMin, topRigMax);

            float midRad = freeLookCam.m_Orbits[1].m_Radius - 1.0f;
            freeLookCam.m_Orbits[1].m_Radius = Mathf.Clamp(midRad, midRigMin, midRigMax);

            float botRad = freeLookCam.m_Orbits[2].m_Radius - 0.6f;
            freeLookCam.m_Orbits[2].m_Radius = Mathf.Clamp(botRad, topRigMin, topRigMax);
        }
    }
}
