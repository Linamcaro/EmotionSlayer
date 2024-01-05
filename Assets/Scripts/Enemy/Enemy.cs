using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public Transform target; // The player's transform
    
    [Header("DAMAGE")]
    [SerializeField] public int touchDamage = 2;
    [SerializeField] public int attackDamage = 1;
    
    [Header("STATS")]
    [SerializeField] public float maxHealth = 20f;
    [SerializeField] private float currentHealth;

    [Header("MOVEMENT")]
    [SerializeField] public float moveSpeed = 3f; // The speed at which the enemy moves   

    private Rigidbody2D rb;
    private GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        // StartOpacityAnimation();
        playerObject = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;
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
        // Attack();
        TouchPlayer();
    }


    private void TouchPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.position);
        if (distanceToPlayer < 3f)
        {
            Debug.LogError("Touched!");
            DamagePlayer(touchDamage);
        }
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void DamagePlayer(int damage)
    {
        PlayerDamage playerHealth = playerObject.GetComponent<PlayerDamage>();
        playerHealth.TakeDamage(damage);
        playerHealth.LogHealth();
    }

    private void MoveEnemy()
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

    void Die()
    {
        Destroy(gameObject,0.5f);
    }
}
