using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriritMovement_Land : MonoBehaviour
{
    public Transform player;

    public Vector3 spawn;
    public float speed;
    private Rigidbody rb;

    public string state;
    private Vector3 moveTo;

    // Start is called before the first frame update
    void Start()
    {
        state = "Spawn";
        spawn = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "OnPlayer")
        {
            moveTo = player.position;
            moveTo.y = transform.position.y;
        }
        else if (state == "ReturnToSpawn")
        {
            moveTo = spawn;
            if (transform.position == spawn) state = "Spawn";
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

    public void ObtainSpiritLand()
    {
        state = "OnPlayer";
    }

    public void ReleaseSpiritLand()
    {
        state = "ReturnToSpawn";

    }
}
