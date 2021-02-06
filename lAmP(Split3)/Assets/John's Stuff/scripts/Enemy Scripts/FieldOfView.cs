using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public GameObject myPlayer;
    public GameObject myBody;
    
    private SpiritHandler spiritRef;
    private Animator animator;
    private Animation_Handler animationRef;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public int seenCounter = 0;
    public Light visionLight;



    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    

    private void Start()
    {
        animator = myBody.GetComponent<Animator>();
        spiritRef = myPlayer.GetComponent<SpiritHandler>();
        animationRef = myBody.GetComponent<Animation_Handler>();
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
           // CheckLight();
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        //CheckLight();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInViewRadius.Length-1; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                //If player is seen, the code below is performed*********************************
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    transform.LookAt(target);
                    animationRef.seenCounter++;
                    spiritRef.loseSpirit();
                    visibleTargets.Add(target);

                    if(animationRef.seenCounter >= 25){
                        animator.SetBool("isDead", true);
                        myPlayer.GetComponent<CharacterController>().enabled = false;
                        myPlayer.GetComponent<PlayerMovement>().enabled = false;
                    }
                }
            }
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void CheckLight()
    {
        for(int r = 0; r < spiritRef.SpiritList.Count-1; r++){
                    if(spiritRef.SpiritList[r].tag == "Spirit_Floating"){
                        visionLight.intensity = 100;
                        visionLight.spotAngle = viewAngle;
                        visionLight.range = viewRadius;
                    }
                    else{
                        visionLight.intensity = 0;
                    }
        }
    }
}
