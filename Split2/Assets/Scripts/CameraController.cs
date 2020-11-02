using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Player;
    public float mouseSensitivity = 100.0f;
    public float maxLookUp = 90.0f;
    public float minLookDown = -90.0f;

    float lookUpRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        lookUpRotation -= mouseY;
        lookUpRotation = Mathf.Clamp(lookUpRotation, minLookDown, maxLookUp);

        transform.localRotation = Quaternion.Euler(lookUpRotation, 0.0f, 0.0f);
        Player.Rotate(Vector3.up * mouseX);

    }
}
