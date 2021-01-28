using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Handler : MonoBehaviour
{
    private Animator animator;
    private bool isWalking = false;
    private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey("space"))
        {
           animator.SetBool("isWalking", false);
           animator.SetBool("isSprinting", false);
           animator.SetBool("isPointing", false);
           animator.SetBool("isJumping", true);
             
        }
        else if(Input.GetKey("z"))
        {
           animator.SetBool("isWalking", false);
           animator.SetBool("isSprinting", false);
           animator.SetBool("isJumping", false);
           animator.SetBool("isPointing", true);
             
        }
        else if ((Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("RUNNING");
            animator.SetBool("isJumping", false);
            animator.SetBool("isSprinting", true);
            animator.SetBool("isPointing", false);
            animator.SetBool("isWalking", false);
             
        }
        else if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") )
        {
            Debug.Log("WALKING");
            animator.SetBool("isJumping", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("isPointing", false);
            animator.SetBool("isSprinting", false);
            
        }
        
        else
        {
            Debug.Log("STANDING");
            animator.SetBool("isJumping", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isSprinting", false);
            animator.SetBool("isPointing", false);
            
        }

        
       
    }
}
