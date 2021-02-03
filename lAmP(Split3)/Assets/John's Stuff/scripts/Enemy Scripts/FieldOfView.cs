using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public GameObject myPlayer;
    private SpiritHandler spiritRef;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public Light visionLight;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    

    private void Start()
    {
        spiritRef = myPlayer.GetComponent<SpiritHandler>();
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
            /*for(int r = 0; r < spiritRef.SpiritList.Length-1; r++){
                    if(spiritRef.SpiritList[r].tag == "Spirit_Floating"){
                        visionLight.intensity = 100;
                        visionLight.spotAngle = viewAngle;
                        visionLight.range = viewRadius;
                    }
                    else{
                        visionLight.intensity = 0;
                    }
            }*/
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                
                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    spiritRef.loseSpirit();
                    visibleTargets.Add(target);
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
