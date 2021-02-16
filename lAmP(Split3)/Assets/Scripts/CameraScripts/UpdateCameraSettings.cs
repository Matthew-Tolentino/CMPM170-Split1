using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCameraSettings : MonoBehaviour
{
    public CameraSettings camSettings;

    [Header("UI Setting Objects")]
    public Toggle xAxis;
    public Toggle yAxis;

    public Slider xSense;
    public Slider ySense;

    public void setXInvert()
    {
        camSettings.xInvert = xAxis.isOn;
    }

    public void setYInvert()
    {
        camSettings.yInvert = yAxis.isOn;
    }

    public void setXSense()
    {
        camSettings.xSense = xSense.value;
    }

    public void setYSense()
    {
        camSettings.ySense = ySense.value;
    }
}
