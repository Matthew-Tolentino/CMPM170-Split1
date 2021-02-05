using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Cinemachine;

public class CameraSettings : MonoBehaviour
{
    public static CameraSettings instance;

    public CinemachineFreeLook freeLookCam;

    [Header("UI Setting Objects")]
    public Toggle xAxis;
    public Toggle yAxis;

    public Slider xSense;
    public Slider ySense;

    public GameObject escMenu;

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

    // Update is called once per frame
    void Update()
    {
        // TODO: Make separate script later to have all player keys and what they do
        //if (Input.GetKeyDown(KeyCode.Escape))
        //{
        //    bool paused = !escMenu.activeSelf;

        //    // Open up Pause Menu
        //    escMenu.SetActive(paused);

        //    // Unlock mouse to use on Menu
        //    if (!paused)
        //    {
        //        Debug.Log("hide cursor");
        //        Cursor.lockState = CursorLockMode.Locked;
        //        Cursor.visible = false;

        //        freeLookCam.m_XAxis.m_InputAxisName = "Mouse X";
        //        freeLookCam.m_YAxis.m_InputAxisName = "Mouse Y";
        //    }
        //    else
        //    {
        //        Cursor.lockState = CursorLockMode.None;
        //        Cursor.visible = true;

        //        // Stop Camera Movement
        //        freeLookCam.m_XAxis.m_InputAxisName = "";
        //        freeLookCam.m_YAxis.m_InputAxisName = "";

        //        // Spirit UI Code update after 1 second to give time to instantiate objects
        //        Invoke(nameof(UpdateUI), 1f);
        //    }
        //}
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
