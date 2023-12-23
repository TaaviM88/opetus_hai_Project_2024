using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemy : MonoBehaviour, IDamageable
{
    public EnemyData enemyData { get; set; } // Assign this in the inspector or when spawning the enemy
    public TMP_Text tmpText;
    public float shakeDuration = 1f;
    private ShakeBehavior shake;
    private int currentHealth;
    private Transform playerTransform;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        shake = GetComponent<ShakeBehavior>();
        shake.enabled = false;
    }

    private void OnEnable()
    {
        
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
    }

    private void Update()
    {
        tmpText.text = currentHealth.ToString();
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
        UIManager.Instance.AddScore(enemyData.scoreValue);
        EnemyPoolManager.Instance.EnemyDefeated(transform.position, enemyData.scoreValue);
        if (Random.value < enemyData.dropChance)
        {
            Instantiate(enemyData.dropPrefab, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
    }

    public void SetHealth()
    {
        currentHealth = enemyData.health;
    }

    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {

            EnemyPoolManager.Instance.SpawnDamageNumber(transform.position, damage);
            shake.enabled = true;
            shake.TriggerShake(shakeDuration);

        }
    }

}
