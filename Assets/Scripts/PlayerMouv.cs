using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class PlayerMouv : MonoBehaviourPun
{
    public Rigidbody controller;
    public float speed = 12f;
    public float jumpHeight = 5f;
    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Vector3 velocity;
    private bool isGrounded;


     
    void Start()
    {
        GetComponent<PlayerMouv>().enabled = true;

    }
    void Update()
    {
        
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;  
        }
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
          
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.AddForce(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.AddForce(jumpHeight*Vector3.up);
        }
             
        if (controller.velocity.magnitude > 8f) 
        {
            controller.velocity = controller.velocity.normalized*8;
        }
    }

   

}
