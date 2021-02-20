using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroller : MonoBehaviour
{

    public Transform[] waypoints;
    public int speed;

    private Animator animator;
    private FieldOfView vision;
    public int tracker = 0;
    private bool isMoving = true;

    private int waypointIndex;
    private float dist;

    private bool reset = false;

    public GameObject player;

    
    void Start()
    {
        animator = GetComponent<Animator>();
        vision = GetComponent<FieldOfView>();
        waypointIndex = 0;
        transform.LookAt(waypoints[waypointIndex].position);
        animator.SetBool("isWalking", true);

    }

    
    void Update()
    {
        //Debug.Log("isSeen: "+vision.isSeen);
        //Debug.Log("isSeen: "+vision.isSeen);
       
        if (vision.isSeen == true)
        {
            reset = true;
            animator.SetBool("isWalking",false);
            isMoving = false;
        }

        else{

            if(reset == true)
            {
                ResetVision();
            }

            dist = Vector3.Distance(transform.position, waypoints[waypointIndex].position);
            if (dist < 1f)
            {
                if (tracker >= 400)
                {
                    
                    animator.SetBool("isWalking", true);
                    isMoving = true;
                    tracker = 0;
                    IncreaseIndex();
                }

                else
                {
                    animator.SetBool("isWalking", false);
                    isMoving = false;
                    tracker++;
                }

            }

            if (isMoving == true)
            {
                
                Patrol();
            }
            
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

    void ResetVision()
    {
        transform.LookAt(waypoints[waypointIndex].position);
        reset = false;
        animator.SetBool("isWalking", true);
        isMoving = true;
    }

    
}
