using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    public EnemyData enemyData { get; set; } // Assign this in the inspector or when spawning the enemy
    private int currentHealth;
    private Transform playerTransform;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    private void OnEnable()
    {
        if(enemyData == null)
        {
            return;
        }
        currentHealth = enemyData.health; // Initialize health when the enemy is spawned
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGameplay()) return;
        // Move towards the player
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * enemyData.speed * Time.deltaTime);
        }
    }
    

   public void Die()
    {
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

}
