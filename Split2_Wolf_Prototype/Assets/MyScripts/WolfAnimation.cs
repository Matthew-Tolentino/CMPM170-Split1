using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isMoving = false;
    private bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            animator.SetBool("run", true);
            
        }
        /*
        else if (Input.GetKey(KeyCode.Space))
        {
            animator.ResetTrigger("attack");

            animator.SetTrigger("attack");

        }
        */
        else
        {
            animator.SetBool("run", false);
            
        }

        
       
    }
}
