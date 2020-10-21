using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Call selected creature's ability 
 *
 * Note: Not sure if this is the best way to implement this
 */

public class PlayerAbilityManager : MonoBehaviour
{
    public GameObject Time_Gerbil;

    private TimeGerbil gib;

    // Start is called before the first frame update
    void Start()
    {
        gib = Time_Gerbil.GetComponent<TimeGerbil>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            gib.useAbility();
        }
    }

    void OnTriggerEnter2D(Collider2D obj)
    {
        Debug.Log("Collided");
        if (obj.CompareTag("PressurePlate"))
        {
            obj.gameObject.GetComponent<PressurePlate>().OpenDoor();
        }
    }

    void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.CompareTag("PressurePlate"))
        {
            obj.gameObject.GetComponent<PressurePlate>().CloseDoor();
        }
    }
}
