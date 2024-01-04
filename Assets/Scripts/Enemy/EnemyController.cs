using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController1 : MonoBehaviour
{
    public Transform target; // The player's transform

    [Header("STATS")]
    [SerializeField] public float health = 20f;
    
    [Header("DAMAGE")]
    [SerializeField] public float touchDamage = 20f;
    [SerializeField] public float attackDamage = 10f;

    [Header("MOVEMENT")]
    [SerializeField] public float moveSpeed = 8f; // The speed at which the enemy moves   

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (target == null)
        {
            // If the target is not set, find the player using their tag
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        Attack();
    }

    void Attack()
    {
        // if (!canAttack) return;
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);


        if (distanceToPlayer < 2f) // Adjust the attack range as needed
        {
            // Perform the attack
            // player.GetComponent<PlayerHealth>().TakeDamage(damage); // Assuming the player has a health script
            // canAttack = false;
            // Invoke("ResetAttackCooldown", attackCooldown);
            DamagePlayer(attackDamage);
        }
    }

    void DamagePlayer(float damage)
    {
        Debug.Log("Player takes " + attackDamage + " damage!");
    }

    // void ResetAttackCooldown()
    // {
    //     canAttack = true;
    // }

    void MoveEnemy()
    {
        if (target != null)
        {
            // Calculate the direction towards the player
            Vector2 direction = target.position - transform.position;

            // Normalize the direction vector to ensure consistent speed in all directions
            direction.Normalize();

            // Move the enemy towards the player
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            // If the target is not set, stop moving
            rb.velocity = Vector2.zero;
        }
    }
}
