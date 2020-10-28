using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritHandler : MonoBehaviour
{
    public static float rotDegree;
    public float rotSpeed;

    private GameObject[] floatingSpirits;

    void Start()
    {
        rotDegree = 0f;
    }


    void Update()
    {
        rotDegree += rotSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spirit_Floating")
        {
            var pull = collision.gameObject.GetComponent<SpiritMovement_Floating>();
            pull.ObtainSpiritFloating();
        }
    }

}
