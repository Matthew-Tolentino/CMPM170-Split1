using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public DoorController doorController;

    public float closeTime = 3.0f;

    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Collided");
        if (obj.CompareTag("Player"))
        {
            doorController.OpenDoor();
        }
    }

    void OnTriggerExit(Collider obj)
    {
        Debug.Log("Exit");
        if (obj.CompareTag("Player"))
        {
            doorController.CloseDoor(closeTime);
        }
    }
}
