using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureControl : MonoBehaviour
{
    public GameObject door;
    

    void OnTriggerEnter(Collider col)
    {
        door.transform.position += new Vector3(0, 50, 0);
    }
}
