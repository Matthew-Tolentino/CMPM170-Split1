using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSettingsHandler : MonoBehaviour
{
    public CinemachineFreeLook freeLookCam;

    public CameraSettings camSettings;

    private string lastXAxis;

    // Start is called before the first frame update
    void Start()
    {
        freeLookCam = GetComponent<CinemachineFreeLook>();
    }

    // Horrible way to update settings but easy fix for now (2/15/2021)
    void FixedUpdate()
    {
        freeLookCam.m_XAxis.m_InvertInput = camSettings.getXInvert();
        freeLookCam.m_YAxis.m_InvertInput = camSettings.getYInvert();

        freeLookCam.m_XAxis.m_MaxSpeed = camSettings.getXSense();
        freeLookCam.m_YAxis.m_MaxSpeed = camSettings.getYSense();

        freeLookCam.m_XAxis.m_InputAxisName = camSettings.inputXAxis;
        freeLookCam.m_YAxis.m_InputAxisName = camSettings.inputYAxis;

        // Ugly logic to stop camera from moving after mouse release or pause
        if (lastXAxis != camSettings.inputXAxis)
        {
            lastXAxis = camSettings.inputXAxis;
            freeLookCam.m_XAxis.m_InputAxisValue = 0f;
            freeLookCam.m_YAxis.m_InputAxisValue = 0f;
        }
    }
}
