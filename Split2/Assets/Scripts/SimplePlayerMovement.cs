using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlayerMovement : MonoBehaviour
{
    private Rigidbody rb;

    public float acceleration = 5f;

    public float maxMoveSpeed = 20;

    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * acceleration * vertical);
        rb.AddForce(transform.right * acceleration * horizontal);

        if (rb.velocity.magnitude > maxMoveSpeed)
            rb.velocity = rb.velocity.normalized * maxMoveSpeed;
    }
}
