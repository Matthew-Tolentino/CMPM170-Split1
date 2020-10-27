using System.Collections;
using System.Collections.Generic;

using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class WorldShift : MonoBehaviour
{
    
    public float shiftToValue = 150f;
    public float shiftBackValue = -150f;
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var transform = this.GetComponent<Transform>();
        var new_position = transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (transform.position.x < 280)
            {
                new_position.x += this.shiftToValue;
                transform.position = new_position;

            }

            else
            {
                new_position.x += this.shiftBackValue;
                transform.position = new_position;
            }

        }
    }
    void OnTriggerEnter()
    {
        this.score += 1;
    }





}
