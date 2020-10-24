using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public BoxCollider doorCollider = null;

    public float closeTime = 3.0f;

    private bool onPressurePlate;

    void OnValidate()
    {
        Initalize();
    }

    // Create Door Object that connects to pressure plate
    void Initalize()
    {
        if (doorCollider == null)
        {
            Debug.Log("Initalize");
            GameObject door = GameObject.CreatePrimitive(PrimitiveType.Cube);
            door.name = "Door";
            door.transform.parent = transform;

            doorCollider = door.GetComponent<BoxCollider>();
        }
    }


    void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Collided");
        if (obj.CompareTag("Player"))
        {
            OpenDoor();
            onPressurePlate = true;
        }
    }

    void OnTriggerExit(Collider obj)
    {
        Debug.Log("Exit");
        if (obj.CompareTag("Player"))
        {
            onPressurePlate = false;
            StartCoroutine(CloseDoorAfterTime(closeTime));   
        }
    }

    private void OpenDoor()
    {
        doorCollider.enabled = false;
    }

    IEnumerator CloseDoorAfterTime(float time)
    {
        if (onPressurePlate) yield break;
        float timer = 0;

        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        

        Debug.Log("Done with Coroutine");
        doorCollider.enabled = true;


    }
}
