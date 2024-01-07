using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float timer;

  
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0;
        }

    }

    private void SpawnEnemy()
    {
        Instantiate(enemy, spawnPosition.position,enemy.transform.rotation);
    }
}
