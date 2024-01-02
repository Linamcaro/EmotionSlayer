using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // reference to parent rigidbody and collisions
    [SerializeField] GameObject playerObject;
   
    [SerializeField] Animator animator;

    private PlayerController playerController;
    private Rigidbody2D playerRb;



    void Awake()
    {
        playerRb = playerObject.GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
    }

    
    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(playerRb.velocity.x));
        animator.SetBool("jump", !playerController.IsGrounded());
        animator.SetBool("attack", playerController.playerAttacked());

    }

    
}
