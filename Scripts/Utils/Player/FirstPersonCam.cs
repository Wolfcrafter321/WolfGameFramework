 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Wolf/Player/FirstPersonCam")]
public class FirstPersonCam : MonoBehaviour
{

    public float mouseSensitivityX = 100f;
    public float mouseSensitivityY = 100f;

    public Transform playerBody;

    float xRot = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRot -= mouseY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

    }
}
