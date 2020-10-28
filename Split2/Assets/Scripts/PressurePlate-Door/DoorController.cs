using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private BoxCollider doorCollider;

    private float doorTimer;

    private float closingTime;

    private bool closingDoor;

    // Temp Variables
    public Renderer rend;
    public Material closed;
    public Material open;

    // Start is called before the first frame update
    void Start()
    {
        doorCollider = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        // Check to see if player is off the plate and if enough time elapsed to close the door
        //Debug.Log(Time.time > closingTime);
        if (closingDoor && Time.time > closingTime)
        {
            doorCollider.enabled = true;
            rend.material = closed;
        }
    }

    public void OpenDoor()
    {
        closingDoor = false;
        doorCollider.enabled = false;
        rend.material = open;
    }

    public void CloseDoor(float timer)
    {
        closingDoor = true;
        doorTimer = timer;
        closingTime = doorTimer + Time.time;
    }   
}
