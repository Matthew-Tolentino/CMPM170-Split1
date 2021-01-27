using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionField : MonoBehaviour
{

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

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
        
        for(int r = 0; r < SpiritRef.SpiritList.Length; r++){
            if(SpiritRef.SpiritList != null && SpiritRef.SpiritList[r] != null ){
                if(SpiritRef.SpiritList[r].tag == "Spirit_Floating"){
                    
                    visionLight.intensity = 100;
                    visionLight.spotAngle = Vision.viewAngle;
                    visionLight.range = Vision.viewRadius;
                }
                
            }
            else{
                
                count = count + 1;
                
            }
            
        }
        
        if(count == SpiritRef.SpiritList.Length){
            
            visionLight.intensity = 0;
            visionLight.spotAngle = 0;
            visionLight.range = 0;
        }
        
    } 
    
}
