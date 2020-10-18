using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
