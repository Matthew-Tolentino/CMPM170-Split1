using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    // Makes CameraSettings into a singleton
    public static CameraSettings instance;

    public CinemachineFreeLook freeLookCam;

    [Header("UI Setting Objects")]
    public Toggle xAxis;
    public Toggle yAxis;

    public Slider xSense;
    public Slider ySense;

    public GameObject escMenu;

    // Make sure there is only 1 CameraSettings
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    private void UpdateUI()
    {
        FindObjectOfType<PausedMenu>().displaySpiritsOnUI();
    }

    public void setCameraControl(bool isEnabled)
    {
        if (isEnabled)
        {
            // Enable Camera Movement
            freeLookCam.m_XAxis.m_InputAxisName = "Mouse X";
            freeLookCam.m_YAxis.m_InputAxisName = "Mouse Y";
        }
        else
        {
            // Stop Camera Movement
            freeLookCam.m_XAxis.m_InputAxisName = "";
            freeLookCam.m_YAxis.m_InputAxisName = "";

            // Prevent camera moving after hitting pause button
            freeLookCam.m_XAxis.m_InputAxisValue = 0f;
            freeLookCam.m_YAxis.m_InputAxisValue = 0f;
        }
    }

    public void toggleXInversion()
    {
        freeLookCam.m_XAxis.m_InvertInput = xAxis.isOn;
    }

    public void toggleYInversion()
    {
        freeLookCam.m_YAxis.m_InvertInput = yAxis.isOn;
    }

    public void setXMouseSense()
    {
        freeLookCam.m_XAxis.m_MaxSpeed = xSense.value;
    }

    public void setYMouseSense()
    {
        freeLookCam.m_YAxis.m_MaxSpeed = ySense.value;
    }
}
