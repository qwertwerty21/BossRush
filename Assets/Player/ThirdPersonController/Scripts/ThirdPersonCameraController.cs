using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] Transform defaultTarget;
    [SerializeField] Transform player;

    private Transform currentTarget;
    private float mouseX;
    private float mouseY;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentTarget = defaultTarget;
    }

    private void LateUpdate()
    {
        CamControl();
    }

    private void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35f, 60f);

      
        transform.LookAt(currentTarget);
        Debug.Log("currentTarget " + currentTarget.name);

       
        currentTarget.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        player.rotation = Quaternion.Euler(0f, mouseX, 0f);
    }

}
