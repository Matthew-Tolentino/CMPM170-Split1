using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHandler : MonoBehaviour
{
    public static float rotDegree;
    public float rotSpeed;

    public GameObject[] SpiritList;

    public bool ability;

    void Start()
    {
        rotDegree = 0f;
        ability = false;
    }


    void Update()
    {
        rotDegree += rotSpeed * Time.fixedDeltaTime;
        //Tester
        //------------------------------------------------------------------
        //if (Input.GetKeyDown("space")) loseSpirit();

        if (Input.GetKeyDown("z")) callAbiliy();
        //------------------------------------------------------------------
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Spirit_Floating")
        {
            var pull = collision.gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.state != "Spawn") return;
            pull.ObtainSpiritFloating();
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

        for (int i = 0; i < 6; ++i)
        {
            if (SpiritList[i] == null)
            {
                SpiritList[i] = collision.gameObject;
                collision.gameObject.GetComponent<FMODUnity.StudioEventEmitter>().Play();
                break;
            }
        }
    


    }

    public void loseSpirit()
    {
        int i = 0;
        while (i < 6)
        {
            if (SpiritList[i] == null)
                ++i;
            else
            {
                if (SpiritList[i].tag == "Spirit_Floating")
                {
                    var pull = SpiritList[i].GetComponent<SpiritMovement_Floating>();
                    pull.ReleaseSpiritFloating();
                    Physics.IgnoreCollision(SpiritList[i].GetComponent<Collider>(), GetComponent<Collider>(), false);
                }
                else if (SpiritList[i].tag == "Spirit_Land")
                {
                    var pull = SpiritList[i].GetComponent<SpriritMovement_Land>();
                    pull.ReleaseSpiritLand();
                }
                SpiritList[i] = null;
                return;
            }
        }
    }

    private void callAbiliy()
    {
        for (int i = 0; i < 6; ++i)
        {
            if (SpiritList[i].tag == "Spirit_Land")
            {
                var pull = SpiritList[i].GetComponent<SpriritMovement_Land>();
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
        }
    }
}
