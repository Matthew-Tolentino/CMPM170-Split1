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

    private Vector3 spawn;
    public float speed;

    public string type;
    private float timer;
    private Collider fs;

    // Dialog Code (Matthew) ---------------------
    [HideInInspector]
    public bool saidDialog = false;
    // -------------------------------------------

    private void Start()
    {
        fs = GetComponent<Collider>();
        spawn = transform.position;
        rb = GetComponent<Rigidbody>();
        state = "Spawn";
        if (type == "") type = "NULL";
        timer = 5f;
    }

    private void Update()
    {
        if (state == "OnPlayer")
        {
            float angle = SpiritHandler.rotDegree * Time.fixedDeltaTime;
            Vector3 raiseHeight = player.transform.position;
            raiseHeight.y += 2;
            moveTo = raiseHeight + new Vector3((radius * Mathf.Cos(angle + initialD)), 0f, (radius * Mathf.Sin(angle + initialD)));
            moveTo.y += Mathf.Cos(angle * numVertFluct + initialF) * verticalFluct;
        }
        else if (state == "ReturningToSpawn")
        {
            moveTo = spawn;
            if (Mathf.Round(transform.position.x) == Mathf.Round(spawn.x) && Mathf.Round(transform.position.z) == Mathf.Round(spawn.z))
            {
                fs.enabled = true;
                state = "Spawn";
            }
        }
    }

    private void LateUpdate()
    {
        if (state != "Spawn")
        {
            Vector3 direction = (moveTo - transform.position).normalized * (moveTo - transform.position).magnitude * speed;
            rb.velocity = direction;
        }
            
    }

    public void ObtainSpiritFloating(int d)
    {   
        state = "OnPlayer";
        initialD = d;
    }

    public void ReleaseSpiritFloating()
    {
        fs.enabled = false;
        state = "ReturningToSpawn";
    }

}
