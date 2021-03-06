﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform Playerbody;
    private float xRotation = 0f;
    private PhotonView photonView;
    
    void Update()
   {

       float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
       float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

       xRotation -= mouseY;
       xRotation = Mathf.Clamp(xRotation, -90, 90f);

       transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
       Playerbody.Rotate(Vector3.up*mouseX);
    }
}
