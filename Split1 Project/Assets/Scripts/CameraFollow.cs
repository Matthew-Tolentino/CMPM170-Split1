using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objToFollow;

    public float moveDuration = 0.5f;

    [Range(5.0f, 10.0f)]
    public float cameraZoom = 10.0f;

    private Vector3 targetPos;

    private bool following = false;

    private Camera cam;


    void Start()
    {
        cam = GetComponent<Camera>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!following)
        {
            targetPos = objToFollow.transform.position;
            targetPos.z = -10.0f;
            StartCoroutine(Follow(targetPos));
        }

        // Camera FOV
        cam.orthographicSize = cameraZoom;
    }

    
    private IEnumerator Follow(Vector3 targetPos)
    {
        following = true;
        float time = 0;
        Vector3 startPos = transform.position;

        while (time < moveDuration)
        {
            // Smooth start and end following
            //float interpolationPoint = time / moveDuration;
            //interpolationPoint = interpolationPoint * interpolationPoint * (3f - 2f * interpolationPoint);

            transform.position = Vector3.Lerp(startPos, targetPos, time / moveDuration);
            time += Time.deltaTime;
            yield return null;
        }
        following = false;
        transform.position = targetPos;
    }
}
