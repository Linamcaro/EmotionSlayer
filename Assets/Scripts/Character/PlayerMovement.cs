using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public int jumpForce = 10;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        const int jumpForce = 5;        // How strong is the jump?
        const int movementForce = 3;    // How strong is the movement?
        if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            myRigidbody.velocity = Vector2.up * jumpForce;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            myRigidbody.velocity = Vector2.up * jumpForce;
        }
        
        myRigidbody.AddForce(transform.right * movementForce * Input.GetAxis("Horizontal"), ForceMode2D.Force);
    }
}
