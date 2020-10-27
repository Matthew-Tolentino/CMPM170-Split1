using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMovement1 : MonoBehaviour
{
    public int score = 0;
    public GameObject spirit;

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

        UnityEngine.Debug.Log("Something collided with us!");
        //Object.Destroy(this.gameObject, 0.0f);

        if (transform.position.x < 280)
        {
            spirit.transform.position = new Vector3(-4.5f, -2.5f, 1.7f);

        }




    }
}


