using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHandler : MonoBehaviour
{
    public static float rotDegree;
    public float rotSpeed;

    public GameObject[] SpiritList;

    void Start()
    {
        rotDegree = 0f;
    }


    void Update()
    {
        rotDegree += rotSpeed * Time.fixedDeltaTime;
        //Tester
        //------------------------------------------------------------------
        if (Input.GetKeyDown("space"))
        {
            loseSpirit();
        }
        //------------------------------------------------------------------
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spirit_Floating")
        {
            var pull = collision.gameObject.GetComponent<SpiritMovement_Floating>();
            if (pull.state != "Spawn") return;
            pull.ObtainSpiritFloating();
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

}
