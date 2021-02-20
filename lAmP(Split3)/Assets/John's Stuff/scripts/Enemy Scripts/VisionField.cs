using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionField : MonoBehaviour
{
    /*
     * This script manages the lights used to show
     * enemy vision when the rabbit spirit is with
     * the player. The script checks to see if the 
     * rabbit spirit is collected, and updates the 
     * enemy lights accordingly. 
    */
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    // Set Up
    public GameObject myPlayer;
    private SpiritHandler SpiritRef;
    
    public GameObject enemySelf;
    private FieldOfView Vision;

    public Light visionLight;

   
    void Start()
    {
        SpiritRef = myPlayer.GetComponent<SpiritHandler>();
        Vision = enemySelf.GetComponent<FieldOfView>();
    }
    
    void Update()
    {
        CheckLight();
    }

    public void CheckLight()
    {
        int count = 0;
        
        for(int r = 0; r < SpiritRef.SpiritList.Count; r++){
            if(SpiritRef.SpiritList != null && SpiritRef.SpiritList[r] != null ){
                // Check if the rabbit spirit is in party
                if(SpiritRef.SpiritList[r].tag == "Spirit_Floating"){
                    // Activate spotlight
                    visionLight.intensity = 100;
                    visionLight.spotAngle = Vision.viewAngle;
                    visionLight.range = Vision.viewRadius;
                }
                
            }
            else{
                count = count + 1;
            }
            
        }
        // Deactivates spotlight
        if(count == SpiritRef.SpiritList.Count){
            visionLight.intensity = 0;
            visionLight.spotAngle = 0;
            visionLight.range = 0;
        }
        
    } 
    
}
