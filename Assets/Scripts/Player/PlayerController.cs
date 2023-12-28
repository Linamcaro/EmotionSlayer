using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("LAYERS")]
    [SerializeField] LayerMask groundLayer;

    /*[Header("INPUT")]
    [SerializeField] private bool SnapInput;
    [SerializeField] private float VerticalDeadZoneThreshold;
    [SerializeField] private float HorizontalDeadZoneThreshold;*/

    [Header("MOVEMENT")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float grounderDistance = 0.2f;
    [SerializeField] private float maxSpeed = 14;
    [SerializeField] private float acceleration = 120;
    [SerializeField] private float groundDeceleration = 60;
    [SerializeField] private float airDeceleration = 30;
    [SerializeField] private float jumpPower = 36;
    [SerializeField] private float maxFallSpeed = 40;
    [SerializeField] private float fallAcceleration = 110;
    [SerializeField] private float groundingForce = -1.5f;
    [SerializeField] private float jumpEndEarlyGravityModifier = 3;

    private FrameInput frameInput;
    private Rigidbody2D playerRb;
    private Vector2 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        //HandleGravity();
        Jump();
        HandleDirection();
        ApplyMovement();
        
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, grounderDistance, groundLayer);
    }

    private void GetInput()
    {
        frameInput = new FrameInput
        {
            jump = PlayerControls.Instance.PlayerJumped(),
            fire = PlayerControls.Instance.PlayerFired(),
            move = PlayerControls.Instance.GetPlayerMovement()

        };
    }

    private void Jump()
    {
        if (IsGrounded() && frameInput.jump)
        {
            playerVelocity.y = jumpPower;

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
            //if (playerVelocity.y > 0) inAirGravity *= jumpEndEarlyGravityModifier;
            playerVelocity.y = Mathf.MoveTowards(playerVelocity.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
        }
    }


    private void HandleDirection()
    {
        if (frameInput.move.x == 0)
        {
            var deceleration = IsGrounded() ? groundDeceleration : airDeceleration;
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, frameInput.move.x * maxSpeed, acceleration * Time.fixedDeltaTime);
        }
    }

    private void ApplyMovement() => playerRb.velocity = playerVelocity;

}


public struct FrameInput
{
   public bool jump;
   public bool fire;
   public Vector2 move;
}
