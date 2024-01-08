using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerCombat : MonoBehaviour
{

    [Header("POSITION")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange;
    
    [Header("DAMAGE")]
    [SerializeField] private int swordDamage;
    [SerializeField] private float attackRate;
    [SerializeField] private float nextAttackTime;

    [Header("TOUCHED")]
    [SerializeField] public float touchDelay = 1.0f;
    [SerializeField] public bool wasTouched = false;

    [SerializeField] private LayerMask enemyLayer;

    private bool fire;

    //Events
    public EventHandler playerFired;

    void Update()
    {
        GetInput();

    }

    private void GetInput()
    {

        fire = PlayerControls.Instance.PlayerFired();
     
        if (Time.time >= nextAttackTime) 
        { 
            if (fire)
            {
                playerFired?.Invoke(this, EventArgs.Empty);
                Attack();
                nextAttackTime = Time.time + 1f/attackRate;
            }
        }
    }

    private void Attack()
    {
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(swordDamage);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    /// <summary>
    /// Return if player is attacking
    /// </summary>
    /// <returns></returns>
    public bool IsAttacking()
    {
        return fire;
    }


    public IEnumerator touched()
    {
        yield return new WaitForSeconds(touchDelay);
        wasTouched = false;
    }

}
