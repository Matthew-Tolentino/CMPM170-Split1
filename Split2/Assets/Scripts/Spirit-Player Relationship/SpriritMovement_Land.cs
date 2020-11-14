using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriritMovement_Land : MonoBehaviour
{
    public Transform player;

    private Vector3 spawn;
    public float speed;
    private Rigidbody rb;

    public string state;
    private Vector3 moveTo;
    private float timer;
    private Collider fs;

    public string type;
    private float accel;

    private Vector3 forceMove;
    private Quaternion forceRot;

    // Start is called before the first frame update
    void Start()
    {
        state = "Spawn";
        spawn = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        timer = 3f;
        fs = GetComponent<Collider>();
        fs.enabled = true;

        if (type == "") type = "NULL";
        accel = 1f;

        forceMove = new Vector3();
        forceRot = new Quaternion();
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
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                rb.useGravity = false;
                fs.enabled = false;
                accel = 5f;

            }
            if (Mathf.Round(transform.position.x) == Mathf.Round(spawn.x) && Mathf.Round(transform.position.z) == Mathf.Round(spawn.z))
            {
                state = "Spawn";
                timer = 3f;
                rb.useGravity = true;
                fs.enabled = true;
                accel = 1f;
            }
        }
        else if (state == "ForceMovement")
        {
            moveTo = forceMove;
            transform.rotation = forceRot;
            rb.useGravity = false;
            if (Mathf.Round(transform.position.x) == Mathf.Round(forceMove.x) && Mathf.Round(transform.position.z) == Mathf.Round(forceMove.z))
            {
                state = "ForcedMovent_Idle";
                rb.useGravity = true;
            }
        }
    }

    private void LateUpdate()
    {
        if (state != "Spawn" && state != "ForcedMovent_Idle")
        {
            Vector3 direction = (moveTo - transform.position).normalized * (moveTo - transform.position).magnitude * speed * accel;
            rb.velocity = direction;
        }
    }

    public void ObtainSpiritLand()
    {
        state = "OnPlayer";
        accel = 1f;
    }

    public void ReleaseSpiritLand()
    {
        state = "ReturnToSpawn";
    }

    public void abilityMove(Vector3 pos, Quaternion rot = new Quaternion(), float changeSpeed = 1f)
    {
        state = "ForceMovement";
        forceMove = pos;
        forceRot = rot;
        accel = changeSpeed;
    }
}
