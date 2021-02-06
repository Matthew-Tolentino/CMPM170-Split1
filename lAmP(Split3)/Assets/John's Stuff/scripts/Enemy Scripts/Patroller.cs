using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{

    public Transform[] waypoints;
    public int speed;

    private Animator animator;
    public int tracker = 0;
    private bool isMoving = true;

    private int waypointIndex;
    private float dist;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
        animator.SetBool("isWalking", true);

    }

    
    void Update()
    {
        dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
        if(dist < 1f)
        {
            if(tracker >= 400){
                animator.SetBool("isWalking", true);
                isMoving = true;
                tracker = 0;
                IncreaseIndex();
            }

            else{
                animator.SetBool("isWalking", false);
                isMoving = false;
                tracker++;
            }

        }
        
        if(isMoving == true){
            Patrol();
         }
        
        
    }

    void Patrol()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        waypointIndex++;
        if(waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
        }
        transform.LookAt(waypoints[waypointIndex].position);
    }

    
}
