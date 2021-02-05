using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
	private Collider c;
	private MeshRenderer mesh;

	public bool isTriggered;

	void Start()
    {
        c = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
        isTriggered = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
    	if (collision.gameObject.tag == "Player")
        {
        	isTriggered = true;
        	c.enabled = false;
    		mesh.enabled = false;
    		GetComponent<DialogueTrigger>().TriggerDialogue();
        }
    }

}
