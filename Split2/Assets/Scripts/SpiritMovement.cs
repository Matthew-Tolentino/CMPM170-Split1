using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritMovement : MonoBehaviour
{
    public Transform player;
    public float radius;
    public float rotationSpeed;
    public float initialD;

    public float verticalFluct;
    public float initialF;

    private Vector3 moveTo;
    private float degree;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        degree = initialD;
    }

    private void Update()
    {
        moveTo = player.transform.position + new Vector3((radius * Mathf.Cos(degree * Time.fixedDeltaTime)), 0f, (radius * Mathf.Sin(degree * Time.fixedDeltaTime)));
        moveTo.y += Mathf.Cos((degree + initialF)* Time.fixedDeltaTime) * verticalFluct;
        degree += rotationSpeed * Time.fixedDeltaTime;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(moveTo);
    }

}
