using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    [Header("Movement Variables")]
    public float speed = 6f;
    public float defaultSpeed = 6f;
    public float sprintSpeed = 20f;
    [Range(0f, 1f)]
    public float turnSpeed = .5f;

    [Header("Ground Checks")]
    public float groundDistance = 1f;
    public LayerMask groundMask;

    // Gravity variables
    public float gravity = -9.81f;
    bool isGrounded;
    Vector3 velocity;

    // Player Rotation variables
    public float turnSmoothTime = .01f;
    float turnSmoothVelocity;

    // Tracking Cam
    bool snapped = false;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Handle Sprint
        if (InputManager.instance.GetKey("Sprint"))
            speed = sprintSpeed;
        else
            speed = defaultSpeed;

        float vert = Input.GetAxisRaw("Vertical");
        float hori = Input.GetAxisRaw("Horizontal");

        // If Dialogue is showing disable movement
        if (DialogueManager.instance.isOpen)
        {
            vert = 0f;
            hori = 0f;
        }

        Vector3 dir = new Vector3(hori, 0f, vert).normalized;

        // Freecam when Click
        if (GameManager.mouseState == GameManager.MouseState.game)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
                GameManager.instance.setMouseLock(true);
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
                GameManager.instance.setMouseLock(false);
        }

        // Rotate Player
        if (dir.magnitude >= 0.1f)
        {
            PlayerTurnTo(dir);
            snapped = false;
        }
        // if not snapped to cam direction, snap to it
        else if (dir.magnitude < 0.1f && !snapped)
        {
            PlayerTurnTo(dir);
            snapped = true;
        }

        // Move Player
        Vector3 moveDir = Mathf.Abs(vert) * transform.forward;
        controller.Move(moveDir.normalized * speed * Time.deltaTime);

        // Gravity Handeling
        Vector3 checkPos = new Vector3(transform.position.x, transform.position.y - 1.6f, transform.position.z);
        if (Physics.CheckSphere(checkPos, groundDistance, groundMask))
        {
            isGrounded = true;
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity + Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void PlayerTurnTo(Vector3 dir)
    {
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }







    //public CharacterController controller;
    //public Transform cam;

    //// Ground check variabled for jumping mechanic
    ///*public Transform groundCheck;
    //public float groundDistance = 0.4f;
    //public LayerMask groundMask;*/

    //public float speed = 6f;
    //public float sprintSpeed = 20f;
    //public float gravity = -9.81f;
    //public float jumpHeight = 3f;

    //// Gravity variables for jumping mechanic
    //Vector3 velocity;
    //public bool isGrounded;

    //public float turnSmoothTime = .01f;
    //float turnSmoothVelocity;
    //float targetAngle;
    //float angle;

    //private float tempAngle;

    //void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    //void Update()
    //{
    //    // Sprinting
    //    if (Input.GetKeyDown(KeyCode.LeftShift)) speed = sprintSpeed;
    //    else if (Input.GetKeyUp(KeyCode.LeftShift)) speed = 6f;

    //    // Player facing and rotation
    //    float horizontal = Input.GetAxisRaw("Horizontal");
    //    float vertical = Input.GetAxisRaw("Vertical");
    //    Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

    //    // Gets rotation of player when free cam is activated
    //    if (Input.GetMouseButtonDown(1))
    //        tempAngle = transform.eulerAngles.y;

    //    if (!Input.GetMouseButton(1))
    //    {
    //        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
    //        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
    //    }
    //    else if (Input.GetMouseButton(1))
    //    {
    //        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + tempAngle;
    //        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
    //    }

    //    // Apply the rotation
    //    transform.rotation = Quaternion.Euler(0f, angle, 0f);

    //    if (direction.magnitude >= 0.1f)
    //    {
    //        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    //        controller.Move(moveDir.normalized * speed * Time.deltaTime);
    //    }

    //    // Gravity Handeling
    //    //if (Physics.CheckSphere(groundCheck.position, groundDistance, groundMask))
    //    //    isGrounded = true;

    //    if (isGrounded && velocity.y < 0)
    //    {
    //        velocity.y = -2f;
    //    }

    //    // Jumping
    //    //if (Input.GetButtonDown("Jump") && isGrounded)
    //    /*{
    //        isGrounded = false;
    //        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    //    }*/

    //    velocity.y += gravity + Time.deltaTime;
    //    controller.Move(velocity * Time.deltaTime);
    //}

    //// Debug Methods
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    //}
}
