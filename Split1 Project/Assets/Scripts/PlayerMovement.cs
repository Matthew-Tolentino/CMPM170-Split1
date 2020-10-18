using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Move player via the keyboard
 *      - Movement is physics based with a rigidbody2D
 *      
 *  Note: Temp movement can switch this out for a character 
 *        controller or something else later.
 */

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement Variables")]
    public float moveSpeed = 5.0f;

    public float maxSpeed = 10.0f;

    private float moveHorizontal = 0.0f;

    private float moveVertical = 0.0f;

    [Header("Rotation Variables")]
    public float rotationSmoothing = 1.0f;

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

        // Handle smooth rotation
        if (movement != Vector2.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 10 * rotationSmoothing * Time.deltaTime);
        }

        // Cap the maximum velocity to maxSpeed
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }
}
