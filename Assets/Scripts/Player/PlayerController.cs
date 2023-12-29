using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("MOVEMENT")]
    [SerializeField] private float grounderDistance = 0.05f;
    [SerializeField] private float maxSpeed = 14;
    [SerializeField] private float acceleration = 120;
    [SerializeField] private float groundDeceleration = 40;
    [SerializeField] private float airDeceleration = 30;
    [SerializeField] private float jumpPower = 36;
    [SerializeField] private float maxFallSpeed = 40;
    [SerializeField] private float fallAcceleration = 110;
    [SerializeField] private float groundingForce = -1.5f;
    [SerializeField] private float jumpEndEarlyGravityModifier = 3;

    private FrameInput frameInput;
    private Rigidbody2D playerRb;
    private Vector2 playerVelocity;
    private CapsuleCollider2D _col;
    [SerializeField] private bool isGrounded;

    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {

        CheckCollisions();
        Jump();
        HandleDirection();
        HandleGravity();
        ApplyMovement();

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

    private void CheckCollisions()
    {
        // Ground and Ceiling
        bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, grounderDistance);
        bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, grounderDistance);

        // Hit a Ceiling
        if (ceilingHit) playerVelocity.y = Mathf.Min(0, playerVelocity.y);

        // Landed on the Ground
        if(!isGrounded && groundHit)
        {
            isGrounded = true;
        }
        else if (isGrounded && !groundHit)
        {
            isGrounded = false;
        }

    }


    private void Jump()
    {
        if (isGrounded && frameInput.jump)
        {
            playerVelocity.y = jumpPower;
        }

     }

    private void HandleDirection()
    {
        if (frameInput.move.x == 0)
        {
            var deceleration = isGrounded ? groundDeceleration : airDeceleration;
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, 0, deceleration * Time.fixedDeltaTime);
        }
        else
        {
            playerVelocity.x = Mathf.MoveTowards(playerVelocity.x, frameInput.move.x * maxSpeed, acceleration * Time.fixedDeltaTime);
        }
    }

    private void HandleGravity()
    {
        if (isGrounded && playerVelocity.y <= 0f)
        {
            playerVelocity.y = groundingForce;
        }
        else
        {
            var inAirGravity = fallAcceleration;
            if (playerVelocity.y > 0) inAirGravity *= jumpEndEarlyGravityModifier;
            playerVelocity.y = Mathf.MoveTowards(playerVelocity.y, -maxFallSpeed, inAirGravity * Time.fixedDeltaTime);
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
