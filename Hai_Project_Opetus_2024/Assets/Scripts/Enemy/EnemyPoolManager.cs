using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager Instance;

    public GameObject enemyPrefab; // The enemy prefab you want to pool
    public int poolSize = 10; // Initial size of the pool

    private Queue<GameObject> enemyPool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.SetActive(false); // Start with the enemy deactivated
            enemyPool.Enqueue(newEnemy);
        }
    }

    public GameObject GetEnemy()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            // Optionally expand the pool if empty
            GameObject newEnemy = Instantiate(enemyPrefab);
            newEnemy.SetActive(true);
            return newEnemy;
        }
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyPool.Enqueue(enemy);
    }
}
