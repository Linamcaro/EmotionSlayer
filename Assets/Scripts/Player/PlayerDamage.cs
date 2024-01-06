using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    [Header("STATS")]
    [SerializeField] public int maxHealth = 20;
    [SerializeField] public int currentHealth;
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void LogHealth()
    {
        Debug.Log("Player Health at" + currentHealth);
    }

    void Die()
    {
        Destroy(gameObject,0.5f);
    }
}
