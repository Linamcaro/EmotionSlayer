using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // reference to parent rigidbody and collisions
    [SerializeField] GameObject playerObject;
    [SerializeField] Animator animator;

    private PlayerController playerController;
    private PlayerCombat playerCombat;
    private Rigidbody2D playerRb;

    void Awake()
    {
        playerRb = playerObject.GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    private void Start()
    {
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.OnPlayerDied += PlayerAnimation_OnPlayerDied;
        }
    }

    void Update()
    {
        animator.SetFloat("speed", Mathf.Abs(playerRb.velocity.x));
        animator.SetBool("jump", !playerController.IsGrounded());
        animator.SetBool("attack", playerCombat.IsAttacking());

    }

    private void PlayerAnimation_OnPlayerDied(object sender, EventArgs e)
    {
        animator.SetTrigger("dead");
    }

    private void OnDestroy()
    {
        if (PlayerHealth.Instance != null)
        {
            PlayerHealth.Instance.OnPlayerDied -= PlayerAnimation_OnPlayerDied;
        }
    }


}

  

