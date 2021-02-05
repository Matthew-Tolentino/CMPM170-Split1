using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHandler : MonoBehaviour
{
	public List<GameObject> SpiritList;
	public static float rotDegree;
    public float rotSpeed;

    public bool triggerLamp;

    private int numFloaters;
    public int selectedSpirit;
    public bool ability;


    void Start()
    {
    	SpiritList = new List<GameObject>();
    	rotDegree = 0f;
    	numFloaters = 0;
    	selectedSpirit = -1;
    	ability = false;
        triggerLamp = false;
    }



    void Update()
    {
    	rotDegree += rotSpeed * Time.fixedDeltaTime;

    	if (Input.GetKeyDown("r")) callAbility();
    	if (Input.GetKeyDown("q")) decrementSelect();
    	if (Input.GetKeyDown("e")) incrementSelect();
    }



    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Spirit_Floating")
        {
            var pull = collision.gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.state != "Spawn") return;
            pull.ObtainSpiritFloating(numFloaters++);
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);

            // Dialog Code (Matthew) ---------------------
            if (!pull.saidDialog) collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            // -------------------------------------------
        }
        else if (collision.gameObject.tag == "Spirit_Land")
        {
            var pull = collision.gameObject.GetComponent<SpriritMovement_Land>();
            if (pull.state != "Spawn") return;
            pull.ObtainSpiritLand();

            // Dialog Code (Matthew) ---------------------
            if (!pull.saidDialog) collision.gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
            // -------------------------------------------
        }
        else return;

        collision.gameObject.GetComponent<FMODUnity.StudioEventEmitter>().Play();

        SpiritList.Add(collision.gameObject);
        if (selectedSpirit == -1) selectedSpirit = 0;
    }



    public void loseSpirit()
    {
        if (SpiritList.Count == 0) return;
        int removeIndex = SpiritList.Count - 1;

        if (SpiritList[removeIndex].tag == "Spirit_Floating")
        {
            var pull = SpiritList[removeIndex].GetComponent<SpiritMovement_Floating>();
            pull.ReleaseSpiritFloating();
            Physics.IgnoreCollision(SpiritList[removeIndex].GetComponent<Collider>(), GetComponent<Collider>(), false);
            --numFloaters;
        }
        else if (SpiritList[removeIndex].tag == "Spirit_Land")
        {
            var pull = SpiritList[removeIndex].GetComponent<SpriritMovement_Land>();
            pull.ReleaseSpiritLand();
        }

        SpiritList.RemoveAt(removeIndex);
        if (removeIndex == selectedSpirit) {
        	--selectedSpirit;
        	if (ability) callAbility();
        }
    }



    private void callAbility()
    {
    	if (SpiritList[selectedSpirit].tag == "Spirit_Land")
    	{
    		var pull = SpiritList[selectedSpirit].GetComponent<SpriritMovement_Land>();
    		if (pull.type == "Sit") 
    		{
    			if (!ability)
    			{
    				Vector3 got = transform.position + transform.forward * 4f;
                    pull.abilityMove(got);
                    ability = true;
    			}
    			else
    			{
    				pull.ObtainSpiritLand();
                    ability = false;
    			}
    			return;
    		}

    		else return;
    	}

    	else if (SpiritList[selectedSpirit].tag == "Spirit_Floating")
    	{
    		var pull = SpiritList[selectedSpirit].gameObject.GetComponent<SpiritMovement_Floating>();
            triggerLamp = true;
    	}
    }



    private void incrementSelect()
    {
    	if (selectedSpirit == -1) return;
    	if (ability) {
        	callAbility();
        	ability = !ability;
        }
    	if (selectedSpirit == SpiritList.Count - 1) selectedSpirit = 0;
    	else ++selectedSpirit;
    }
    private void decrementSelect()
    {
    	if (selectedSpirit == -1) return;
    	if (ability) {
        	callAbility();
        	ability = !ability;
        }
    	if (selectedSpirit == 0) selectedSpirit = SpiritList.Count - 1;
    	else --selectedSpirit;
    }

}