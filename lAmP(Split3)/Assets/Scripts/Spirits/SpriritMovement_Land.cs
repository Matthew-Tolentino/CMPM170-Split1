using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriritMovement_Land : MonoBehaviour
{
	public GameObject pl;
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

    private bool addF;

    // Dialog Code (Matthew) ---------------------
    [HideInInspector]
    public bool saidDialog = false;
    // -------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        state = "Spawn";
        spawn = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        timer = 5f;
        fs = GetComponent<Collider>();
        fs.enabled = true;

        if (type == "") type = "NULL";
        accel = 1f;

        forceMove = new Vector3();
        forceRot = new Quaternion();

        addF = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == "OnPlayer")
        {
            moveTo = player.position;
            rb.isKinematic = false;
            //moveTo.y = transform.position.y;
            if (Vector3.Distance(transform.position, player.position) < 2f) {
                state = "OnPlayer_Idle";
                accel = 1f;
                timer = 5f;
            }
            timer -= Time.deltaTime;
            if (timer <= 0 ) {
                rb.detectCollisions = false;
                timer = 5f;
            }
        }
        else if (state == "OnPlayer_Idle")
        {   
            rb.detectCollisions = true;
            moveTo = transform.position;
            rb.useGravity = true;
            fs.enabled = true;
            rb.isKinematic = true;
            if (Vector3.Distance(transform.position, player.position) > 3f) {
                state = "OnPlayer";
                timer = 5f;
            }
        }
        else if (state == "ReturnToSpawn")
        {
            moveTo = spawn;
            if (timer > 0) timer -= Time.deltaTime;
            else
            {
                rb.detectCollisions = false;
                rb.useGravity = false;
                fs.enabled = false;
                accel = 5f;

            }
            if (Mathf.Round(transform.position.x) == Mathf.Round(spawn.x) && Mathf.Round(transform.position.z) == Mathf.Round(spawn.z))
            {
                state = "Spawn";
                timer = 5f;
                rb.useGravity = true;
                fs.enabled = true;
                rb.detectCollisions = true;
                accel = 1f;
            }
        }
        else if (state == "ForceMovement")
        {   
        	rb.isKinematic = false;
            moveTo = forceMove;
            transform.rotation = forceRot;
            if (Mathf.Round(transform.position.x) == Mathf.Round(forceMove.x) && Mathf.Round(transform.position.z) == Mathf.Round(forceMove.z))
            {
                state = "ForcedMovent_Idle";
                accel = 1f;
                rb.detectCollisions = true;
                fs.enabled = true;
            }
        }

        transform.rotation = Quaternion.LookRotation((player.position - transform.position).normalized);
    }

    private void LateUpdate()
    {
        if (state != "Spawn" && state != "ForcedMovent_Idle" && state != "OnPlayer_Idle")
        {
            Vector3 direction = (moveTo - transform.position).normalized * (moveTo - transform.position).magnitude * speed * accel;
            rb.velocity = direction;
        }
    }
    private void FixedUpdate()
    {
        if (addF)
        {
            //rb.AddForce(Vector3.up * 20);
            addF = false;
        }
    }

    public void ObtainSpiritLand()
    {
    	Physics.IgnoreCollision(pl.GetComponent<Collider>(), GetComponent<Collider>(), true);
        state = "OnPlayer";
        accel = 5f;
    }

    public void ReleaseSpiritLand()
    {
    	Physics.IgnoreCollision(pl.GetComponent<Collider>(), GetComponent<Collider>(), false);
        state = "ReturnToSpawn";
    }

    public void abilityMove(Vector3 pos, Quaternion rot = new Quaternion())
    {
        state = "ForceMovement";
        forceMove = pos;
        //forceMove.y += 0.5f;
        forceRot = rot;
        accel = 5f;
        fs.enabled = false;
        addF = true;
        rb.detectCollisions = false;
    }


}
