using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Light lt1;
    public Light lt2;
    public Light lt3;
    public Light lt4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var transform = this.GetComponent<Transform>();
        var new_position = transform.position;

        if (lt1.intensity == 10 && lt2.intensity == 10 && lt3.intensity == 10 && lt4.intensity == 10)
        {
            new_position.y += 500;
            transform.position = new_position;
        }



        

        
    }
}
