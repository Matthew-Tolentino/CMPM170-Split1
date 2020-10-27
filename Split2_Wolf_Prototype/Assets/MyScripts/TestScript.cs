using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    int n;
    public void OnButtonPress()
    {
        n++;
        UnityEngine.Debug.Log("Button clicked " + n + " times.");
    }
}
