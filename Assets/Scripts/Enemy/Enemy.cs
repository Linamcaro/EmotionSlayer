using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("ANIMATION")]
    [SerializeField] public float fadeInDuration = 1f;
    [SerializeField] public float fadeOutDuration = 2f;

    [Header("DAMAGE")]
    [SerializeField] public int touchDamage = 1;
    [SerializeField] public int attackDamage = 1;
    [SerializeField] public float touchDuration = 0.5f;
    [SerializeField] public float knockBackDuration = 20f;
    [SerializeField] public float knockBackForce = 20f;

    [Header("STATS")]
    [SerializeField] public float maxHealth = 20f;
    [SerializeField] private float currentHealth;

    [Header("MOVEMENT")]
    [SerializeField] public float moveSpeed = 5f; // The speed at which the enemy moves   

    private bool isDead = false;
    private bool canDamagePlayerByTouch = true;
    private Rigidbody2D rb;
    private GameObject playerObject;
    private SpriteRenderer spriteRenderer;
    private Transform target; // The player's transform

    void Start()
    {
        PlayerHealth.Instance.OnPlayerDied += Enemy_OnPlayerDied;
        playerObject = GameObject.FindWithTag("Player");
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f) ;

        if (target == null)
        {
            // If the target is not set, find the player using their tag
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        FadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
        // Attack();
        TouchPlayer();
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;
        
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
            isDead = true;
            touchDamage = 0;
            attackDamage = 0;
        } else
        {
            hitVisual(gameObject.GetComponentInChildren<SpriteRenderer>());
            // hitKnockback(rb);
        }
    }

    private void FadeIn()
    {
        Color targetColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);
        StartCoroutine(FadeTo(targetColor, fadeInDuration));
    }

    private IEnumerator FadeOut()
    {
        Color targetColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            spriteRenderer.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // Ensure the sprite reaches the fully transparent state
        spriteRenderer.color = new Color(targetColor.r, targetColor.g, targetColor.b, 0f);
        Destroy(this);
    }

    private IEnumerator FadeTo(Color targetColor, float fadeDuration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            //Debug.LogError("    Fade: " + alpha + "\n    Time: " + elapsedTime);

            spriteRenderer.color = new Color(targetColor.r, targetColor.g, targetColor.b, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the sprite reaches the fully opaque state
        spriteRenderer.color = new Color(targetColor.r, targetColor.g, targetColor.b, 1f);
    }


    private void TouchPlayer()
    {
        if (isDead) return;
        if (!canDamagePlayerByTouch) return;

        PlayerCombat playerCombat = playerObject.GetComponent<PlayerCombat>();
        if (playerCombat.wasTouched) return;

        playerCombat.wasTouched = true;
        StartCoroutine(playerCombat.touched());

        float distanceToPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
        if (distanceToPlayer < 3f)
        {
            //Debug.LogError("Touched!");
            //DamagePlayer(touchDamage);

            PlayerHealth.Instance.TakeDamage(touchDamage);
            canDamagePlayerByTouch = false;
            hitVisual(playerObject.GetComponentInChildren<SpriteRenderer>());
        }
    }

    /*private void DamagePlayer(int damage)
    {
        if (isDead) return;

        PlayerDamage playerHealth = playerObject.GetComponent<PlayerDamage>();
        playerHealth.TakeDamage(damage);
       playerHealth.LogHealth();
        hitVisual(playerObject.GetComponentInChildren<SpriteRenderer>());
       //itKnockback(playerObject.GetComponent<Rigidbody2D>());


    }*/

    private void hitVisual(SpriteRenderer currentSpriteRenderer)
    {
        currentSpriteRenderer.color = new Color(1f, 0f, 0f, 1f);
        StartCoroutine(fadeBackToNormal(currentSpriteRenderer));
    }

    private IEnumerator fadeBackToNormal (SpriteRenderer currentSpriteRenderer)
    {
        float elapsedTime = 0f;
        float fadeDuration = touchDuration;
        while (elapsedTime < fadeDuration)
        {
            float colorStrength = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            float alpha = currentSpriteRenderer.color.a;
            currentSpriteRenderer.color = new Color(1f, colorStrength, colorStrength, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        canDamagePlayerByTouch = true;
    }

    private void hitKnockback(Rigidbody2D currentRB)
    {
        // Calculate the direction from the enemy to the player
        //Vector2 knockbackDirection = (playerObject.transform.position - rb.transform.position).normalized;
        Debug.LogError("    hitKnockback: " + currentRB);
        currentRB.velocity = new Vector2(knockBackForce, knockBackForce);
    }

    private void MoveEnemy()
    {
        if (isDead) return;

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
        CapsuleCollider2D capsuleCollider = GetComponent<CapsuleCollider2D>();

        Destroy(rb);
        Destroy(capsuleCollider);

        StartCoroutine(FadeOut());
    }

    private void Enemy_OnPlayerDied(object sender, EventArgs e)
    {
        this.enabled = false;
    }
}
