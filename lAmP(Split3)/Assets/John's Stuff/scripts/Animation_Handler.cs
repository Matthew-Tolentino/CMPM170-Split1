using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Animation_Handler : MonoBehaviour
{
    private Animator animator;
    public Image healthBar;
    

    private int angerCounter = 0;
    public float seenCounter = 0;
    private float startHealth = 1;



    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Used to determine current value of health bar. 
        healthBar.fillAmount = ((startHealth-(seenCounter/100)*4)/startHealth);
        
        if(Input.GetKey("space"))
        {
           angerCounter = 0;
           animator.SetBool("isAngry", false);
           animator.SetBool("isWalking", false);
           animator.SetBool("isSprinting", false);
           animator.SetBool("isPointing", false);
           animator.SetBool("isJumping", true);
             
        }
        else if(Input.GetKey("r"))
        {
           angerCounter = 0;
           animator.SetBool("isAngry", false);
           animator.SetBool("isWalking", false);
           animator.SetBool("isSprinting", false);
           animator.SetBool("isJumping", false);
           animator.SetBool("isPointing", true);
             
        }
        else if ((Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d")) && Input.GetKey(KeyCode.LeftShift))
        {
            //Debug.Log("RUNNING");
            angerCounter = 0;
            animator.SetBool("isAngry", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isSprinting", true);
            animator.SetBool("isPointing", false);
            animator.SetBool("isWalking", false);
             
        }
        else if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d") )
        {
            //Debug.Log("WALKING");
            angerCounter = 0;
            animator.SetBool("isAngry", false);
            animator.SetBool("isJumping", false);
            animator.SetBool("isWalking", true);
            animator.SetBool("isPointing", false);
            animator.SetBool("isSprinting", false);
            
        }
        
        else
        {
            //Debug.Log("STANDING");
            angerCounter++;

            animator.SetBool("isJumping", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isSprinting", false);
            animator.SetBool("isPointing", false);

            if(angerCounter >= 600){
                animator.SetBool("isAngry", true);
            }
            
        }

        
       
    }


}
