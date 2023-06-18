using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByMuose : MonoBehaviour
{
    public float rotateSpeed;
    public Transform cameraHoleder;

    public float minPitch;
    public float maxPitch;

    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        UpdateYaw();
        UpdatePicht();
    }
    private void UpdateYaw()
    {
        float hInput = Input.GetAxis("Mouse X");
        transform.Rotate(0, hInput * rotateSpeed, 0);
    }
    private void UpdatePicht()
    {
        float vInput = Input.GetAxis("Mouse Y");
        pitch -= vInput * rotateSpeed;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        cameraHoleder.localRotation = Quaternion.Euler(pitch, 0, 0);
    }
}
