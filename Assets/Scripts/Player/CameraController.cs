using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skrupt od kontroli kamery
//Krzysiek
public class CameraController : MonoBehaviour
{
    public Transform Camera;
    public Transform Giwera;
    public float sensivity;
    float xRotation;

    // Update is called once per frame
    void Update()
    {
        float mouseX=Input.GetAxis("Mouse X")*sensivity*Time.deltaTime;
        float mouseY= Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        Camera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        //Giwera.localRotation = Quaternion.Euler(-xRotation, 200, 0);
    }
}
