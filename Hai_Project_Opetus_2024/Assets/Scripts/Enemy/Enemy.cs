using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemy : MonoBehaviour, IDamageable
{
    public EnemyData enemyData { get; set; } // Assign this in the inspector or when spawning the enemy
    public TMP_Text tmpText;
    public float shakeDuration = 1f;
    public float attackRange = 10f;
    private ShakeBehavior shake;
    private Transform playerTransform;
    private Rigidbody2D rb;
    //Health
    private int currentHealth;
    private float currentSpeed;
    private int currentDamage;
    private SpriteRenderer spriteRenderer;
    //upgrade
    private int upgradeIndex = 0;
    private Vector2 lastRBForce = Vector2.zero;
    // attack
    public float attackCooldown = 2f; // Time between attacks
    public float dashSpeed = 20f; // Speed of the dash attack
    public float dashDuration = 0.5f; // How long the dash lasts
    private float attackTimer = 0f; // Timer to track cooldowns
    private bool isDashing = false; // Is the enemy currently dashing?

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        spriteRenderer = GetComponent<SpriteRenderer>();
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
       
        //add shake when doing attack
        if(CheckGameState() == false || shake.enabled)
        {
            return;
        }

        // Handle attack cooldown
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
        else if (!isDashing && Vector2.Distance(transform.position, playerTransform.position) < attackRange) // Check if the player is close enough
        {
            StartCoroutine(DashAttack());
        }

        // Move towards the player
        if (!isDashing && playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * currentSpeed * Time.deltaTime);
        }
    }
    
    bool CheckGameState()
    {
        if (!GameManager.Instance.IsGameplay())
        {
            lastRBForce = rb.totalForce;
            rb.isKinematic = true;
            return false;
        }
        else
        {
            if (lastRBForce != Vector2.zero)
            {
               
                rb.totalForce = lastRBForce;
                lastRBForce = Vector2.zero;
            }
            rb.isKinematic = false;
            return true;
        }
    }

    private IEnumerator DashAttack()
    {
        isDashing = true;
        float startTime = Time.time;

        // Temporarily make the enemy's collider a trigger to avoid physical collision stopping the dash
        Collider2D collider = GetComponent<Collider2D>();
        bool originalTriggerState = collider.isTrigger;
        collider.isTrigger = true;

        Vector2 targetDirection = (playerTransform.position - transform.position).normalized;

        while (Time.time < startTime + dashDuration)
        {
            // Continue in the initial direction of the dash
            rb.velocity = targetDirection * dashSpeed;
            yield return null;
        }

        // Stop the dash and reset the collider
        rb.velocity = Vector2.zero;
        isDashing = false;
        collider.isTrigger = originalTriggerState;
        attackTimer = attackCooldown; // Reset the attack cooldown
    }

    public void Die()
    {
        UIManager.Instance.AddScore(enemyData.scoreValue);
        EnemyPoolManager.Instance.EnemyDefeated(transform.position, enemyData.scoreValue);
        if (Random.value < enemyData.dropChance)
        {
            GameObject drop = enemyData.GetRandomDrop();
            if (drop != null)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
        }
        gameObject.SetActive(false);
    }

    public void SetStats( int upgradeLevel)
    {
        upgradeIndex = upgradeLevel;
        currentHealth = enemyData.health;
        currentSpeed = enemyData.speed;
        currentDamage = enemyData.damage;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white; // Default color
        UpgradeEnemy();
    }

    public void UpgradeEnemy()
    {
        // Check if the enemy can be upgraded further
        if (upgradeIndex < enemyData.upgrades.Count)
        {
            // Apply the next upgrade
            EnemyUpgrade upgrade = enemyData.upgrades[upgradeIndex];
            currentHealth += upgrade.additionalHealth;
            currentSpeed += upgrade.additionalSpeed;
            currentDamage += upgrade.additionalDamage;
            spriteRenderer.color = upgrade.color; // Change color to indicate the upgrade

        }
        else if (upgradeIndex >= enemyData.upgrades.Count)
        {
            // If the enemy is already at the highest upgrade level, ensure it has the max upgrade values
            EnemyUpgrade maxUpgrade = enemyData.upgrades[enemyData.upgrades.Count - 1];
            currentHealth = enemyData.health + maxUpgrade.additionalHealth;
            currentSpeed = enemyData.speed + maxUpgrade.additionalSpeed;
            currentDamage = enemyData.damage + maxUpgrade.additionalDamage;
            spriteRenderer.color = maxUpgrade.color; // Set to the color of the max upgrade

          
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Player") && isDashing)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(currentDamage);
            // Don't stop the enemy; it should continue its dash
        }
    }
}
}
