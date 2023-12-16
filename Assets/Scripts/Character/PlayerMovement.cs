using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float movementForce = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.AddForce(transform.right * movementForce * Input.GetAxis("Horizontal"), ForceMode2D.Force);
        
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            myRigidbody.velocity = Vector2.up * 10;
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            myRigidbody.velocity = Vector2.down * 10;
        }
    }
}
