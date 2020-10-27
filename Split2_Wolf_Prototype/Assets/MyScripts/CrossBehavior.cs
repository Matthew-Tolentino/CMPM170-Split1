using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CrossBehavior : MonoBehaviour
{
    public int score = 0;
    public Light lt;

    // Start is called before the first frame update
    void Start()
    {
        var transform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //var transform = this.GetComponent<Transform>();
        // this.transform.localRotation *= Quaternion.Euler(0.0f, 100.0f * Time.deltaTime, 0.0f);
    }

    void OnTriggerEnter()
    {
        var new_position = this.transform.position;
        UnityEngine.Debug.Log("Something collided with us!");
        //Object.Destroy(this.gameObject, 0.0f);

        if (transform.position.x < 280)
        {
            new_position.x += 1500f;
            this.transform.position = new_position;
            lt.intensity = 10;

        }




    }
}