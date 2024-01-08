using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemy.GetEnemyHealth() == 0)
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }
}
