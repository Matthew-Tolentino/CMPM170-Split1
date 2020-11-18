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
        if (Input.GetKeyDown("space")) loseSpirit();

        if (Input.GetKeyDown("z")) callAbiliy();
        //------------------------------------------------------------------
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spirit_Floating")
        {
            var pull = collision.gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.state != "Spawn") return;
            pull.ObtainSpiritFloating();
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        else if (collision.gameObject.tag == "Spirit_Land")
        {
            var pull = collision.gameObject.GetComponent<SpriritMovement_Land>();
            if (pull.state != "Spawn") return;
            pull.ObtainSpiritLand();
        }
        else return;

        for (int i = 0; i < 6; ++i)
        {
            if (SpiritList[i] == null)
            {
                SpiritList[i] = collision.gameObject;
                break;
            }
        }
    


    }

    private void loseSpirit()
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
                    Physics.IgnoreCollision(SpiritList[i].GetComponent<Collider>(), GetComponent<Collider>(), true);
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
                    pull.abilityMove(new Vector3(0.0f, 1.0f, -4.0f));
                    ability = true;
                }
                else pull.ObtainSpiritLand();
                return;
            }
        }
    }

}
