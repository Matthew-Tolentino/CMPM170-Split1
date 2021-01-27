using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureControl : MonoBehaviour
{
    public GameObject door;
    public GameObject myPlayer;
    
    public bool onButton;
    
    private SpiritHandler spiritRef;
    
    public GameObject[] lastTouched;

    private int current;
    private Vector3 init;

    // Dialog Code
    [HideInInspector]
    public bool saidDialog = false;

    private void Start()
    {
        current = 0;
        spiritRef = myPlayer.GetComponent<SpiritHandler>();
        onButton = false;
        init = door.transform.position;
    }

    private void Update()
    {
        if (current != 0){}
            onButton = false;
            for (int i = 0; i < current; ++i){
                float distance = Vector3.Distance(transform.position, lastTouched[i].transform.position);
                if (distance < 3.5f) {
                    onButton = true;
                    break;
                }
            }
        if (onButton) door.transform.position = new Vector3(0, -50, 0);
        else door.transform.position = init;
    }

    void OnTriggerEnter(Collider col)
    {
        saidDialog = true;
        if (col.gameObject.tag == "Spirit_Land"){
            //door.transform.position = new Vector3(0, -50, 0);
            for (int i = 0; i < current; ++i){
                if (GameObject.ReferenceEquals(col.gameObject, lastTouched[i])) return;
            }
            lastTouched[current] = col.gameObject;
            ++current;    
        }
        /*
        for(int i = 0; i < spiritRef.SpiritList.Length; i++)
        {
            if(spiritRef.SpiritList[i] != null && spiritRef.SpiritList[i].tag == "Spirit_Land")
            {   
                door.transform.position += new Vector3(0, 50, 0);
                break;
            }
        }
        */
    }
    
}
