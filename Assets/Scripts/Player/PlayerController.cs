using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("MOVEMENT")]
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float jumpPower;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float ceilingCheckRadius;
    [SerializeField] private float groundDeceleration;
    [SerializeField] private float airDeceleration;
    [SerializeField] private float groundingForce;
    [SerializeField] private float fallAcceleration;
    [SerializeField] private float maxfallSpeed;

    [Header("COLLISIONS")]
    // reference to the ground layer objects
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform ceilingCheck;

    private bool isFacingRight = true;
    private Vector2 playerVelocity;
    private Rigidbody2D playerRb;
    private FrameInput frameInput;

   
 
    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Movement();
        Jump();
    }

    private void FixedUpdate()
    {
        HandleGravity();
    }

    /// <summary>
    /// Gather players input
    /// </summary>
    private void GetInput()
    {
        frameInput = new FrameInput
        {
            move = PlayerControls.Instance.GetPlayerMovement(),
            jump = PlayerControls.Instance.PlayerJumped(),
            fire = PlayerControls.Instance.PlayerFired()
        };

    }

    /// <summary>
    /// Perform jump
    /// </summary>
    private void Jump()
    {
        if (frameInput.jump && IsGrounded())
        {
    
            playerVelocity.y = jumpPower;
        }

        if(!IsGrounded() && HitCeiling()) 
        {
            playerVelocity.y = Mathf.Min(0, playerVelocity.y);
        }
    }

 
    private void HandleGravity()
    {

        if (IsGrounded() && playerVelocity.y <= 0f)
        {
            playerVelocity.y = groundingForce;
            
        }
        else
        {
            var inAirGravity = fallAcceleration;
            playerVelocity.y = Mathf.MoveTowards(playerVelocity.y, -maxfallSpeed, inAirGravity * Time.deltaTime);
        }
    }

    /// <summary>
    /// Handle Player Movement
    /// </summary>
    private void Movement()
    {
        if (frameInput.move.x == 0)
        {
            var deceleration = IsGrounded() ? groundDeceleration : airDeceleration;
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, 0, deceleration * Time.deltaTime);
        }
        else if (IsGrounded() && !HitCeiling())
        {
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, frameInput.move.x * maxSpeed, acceleration * Time.fixedDeltaTime);
            
        }


        if (!isFacingRight && frameInput.move.x > 0)
        {
            Flip();
        }
        else if (isFacingRight && frameInput.move.x < 0)
        {
            Flip();
        }

        playerRb.velocity = playerVelocity;
    }

    /// <summary>
    /// Check if player is on the ground
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);  

    }
    
    //---- 

    /// <summary>
    /// Check if the player hit a ceiling
    /// </summary>
    /// <returns></returns>
    private bool HitCeiling()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, groundLayer);
    }
    

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public bool playerAttacked()
    {
        return frameInput.fire;
    }
  
}

public struct FrameInput
{
   public bool jump;
   public bool fire;
   public Vector2 move;
}
