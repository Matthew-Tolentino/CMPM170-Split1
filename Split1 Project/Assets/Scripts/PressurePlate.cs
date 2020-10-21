using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject door;

    private SpriteRenderer sr;

    private BoxCollider2D doorCollider;
    // Start is called before the first frame update
    void Start()
    {
        sr = door.GetComponent<SpriteRenderer>();
        doorCollider = door.GetComponent<BoxCollider2D>();
    }

    public void OpenDoor()
    {
        sr.color = Color.green;
        doorCollider.enabled = false;
    }

    public void CloseDoor()
    {
        sr.color = Color.red;
        doorCollider.enabled = true;
    }
}
