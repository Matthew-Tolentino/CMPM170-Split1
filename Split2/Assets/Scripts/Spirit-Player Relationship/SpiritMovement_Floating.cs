using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMovement_Floating : MonoBehaviour
{
    public Transform player;
    public float radius;
    public float initialD;

    public float verticalFluct;
    public float initialF;
    public float numVertFluct;

    private Vector3 moveTo;
    private Rigidbody rb;
    public string state;

    public Vector3 spawn;
    public float speed;

    private void Start()
    {
        spawn = transform.position;
        rb = GetComponent<Rigidbody>();
        state = "Spawn";
    }

    private void Update()
    {
        if (state == "OnPlayer")
        {
            float angle = SpiritHandler.rotDegree * Time.fixedDeltaTime;
            moveTo = player.transform.position + new Vector3((radius * Mathf.Cos(angle + initialD)), 0f, (radius * Mathf.Sin(angle + initialD)));
            moveTo.y += Mathf.Cos(angle * numVertFluct + initialF) * verticalFluct;
        }
        else if (state == "ReturningToSpawn")
        {
            moveTo = spawn;
            if (transform.position == spawn)
            {
                Collider fs = GetComponent<Collider>();
                fs.enabled = true;
                state ="Spawn";
            }
        }
    }

    private void FixedUpdate()
    {
        if (state != "Spawn")
        {
            Vector3 direction = (moveTo - transform.position).normalized * (moveTo - transform.position).magnitude * speed;
            rb.velocity = direction;

        }
            
    }

    public void ObtainSpiritFloating()
    {   
        state = "OnPlayer";
    }

    public void ReleaseSpiritFloating()
    {
        Collider fs = GetComponent<Collider>();
        fs.enabled = false;
        state = "ReturningToSpawn";
    }
}
