using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "CameraSettings")]
public class CameraSettings : ScriptableObject
{
    public bool xInvert, yInvert;
    public float xSense, ySense;
    public string inputXAxis, inputYAxis;

    public float getXSense()
    {
        return xSense;
    }

    public float getYSense()
    {
        return ySense;
    }

    public bool getXInvert()
    {
        return xInvert;
    }

    public bool getYInvert()
    {
        return yInvert;
    }
}
