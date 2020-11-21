using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureControl : MonoBehaviour
{
    public GameObject door;

    public GameObject myPlayer;
    private SpiritHandler spiritRef;

    private void Start()
    {
        spiritRef = myPlayer.GetComponent<SpiritHandler>();
    }

    void OnTriggerEnter(Collider col)
    {
        for(int i = 0; i < spiritRef.SpiritList.Length; i++)
        {
            if(spiritRef.SpiritList[i] != null && spiritRef.SpiritList[i].tag == "Spirit_Land")
            {
                door.transform.position += new Vector3(0, 50, 0);
                break;
            }
        }
        
    }
}
