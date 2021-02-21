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

    	if (InputManager.instance.KeyDown("SpiritAbility")) callAbility();
    	if (InputManager.instance.KeyDown("SpiritDec")) decrementSelect();
    	if (InputManager.instance.KeyDown("SpiritInc")) incrementSelect();
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

        //collision.gameObject.GetComponent<FMODUnity.StudioEventEmitter>().Play();
        foreach (var emitter in collision.gameObject.GetComponents<FMODUnity.StudioEventEmitter>())
        {
            emitter.Play();
        }

        SpiritList.Add(collision.gameObject);
        if (selectedSpirit == -1) selectedSpirit = 0;
        nameSelected();
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
        nameSelected();
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
            if (pull.type == "Lamp") triggerLamp = true;
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
    	nameSelected();

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
    	nameSelected();
    }

    private void nameSelected(){
    	var send = GetComponent<PlayerUI>();
    	string type = "";
    	if (selectedSpirit == -1) type = "none";
    	else if (SpiritList[selectedSpirit].tag == "Spirit_Land")
    	{
    		var pull = SpiritList[selectedSpirit].GetComponent<SpriritMovement_Land>();
    		if (pull.type == "Sit") type = "Rock";
    	}
    	else if (SpiritList[selectedSpirit].tag == "Spirit_Floating")
    	{
    		var pull = SpiritList[selectedSpirit].gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.type == "Bunny") type = "Bunny";
            if (pull.type == "Lamp") type = "Lamp";
    	}
    	if (type == "") type = "undefined";

    	send.SelectSpirit(type);
    }


}