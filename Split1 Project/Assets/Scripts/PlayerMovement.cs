using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    public float moveSpeed = 5.0f;

    public float maxSpeed = 10.0f;

    private float moveHorizontal = 0.0f;

    private float moveVertical = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // Get horizontal and vertical movement and apply force to rigidbody
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.AddForce(movement * moveSpeed, ForceMode2D.Impulse);

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
