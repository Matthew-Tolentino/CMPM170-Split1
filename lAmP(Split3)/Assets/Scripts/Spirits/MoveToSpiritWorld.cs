using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToSpiritWorld : MonoBehaviour
{
	public string state;
	public bool touching;
	public Vector3 origin;
	public float distance;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        state = "Human";
        touching = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(origin, player.transform.position);
        var pull = player.GetComponent<SpiritHandler>();

        if (pull.triggerLamp) lampAbility();
        if (distance >= 20f && state == "Spirit"){
        	var script = GetComponentsInChildren<ToSpiritWorldController>();
        	foreach (ToSpiritWorldController sc in script){
				if (sc != null) sc.ToHuman();
        	}
        }
    }

    void lampAbility(){
        var pull = player.GetComponent<SpiritHandler>();
        pull.triggerLamp = false;
        if (touching){
            var script = GetComponentsInChildren<ToSpiritWorldController>();
            foreach (ToSpiritWorldController sc in script){
                if (sc != null) sc.ToSpirit();
            }
            state = "Spirit";
        }
    }

    void OnTriggerStay(Collider col){
    	if (col.gameObject.tag == "Player"){
    		touching = true;
    	}
    }
    void OnTriggerExit(Collider col){
    	if (col.gameObject.tag == "Player"){
    		touching = false;
    	}
    }
}
