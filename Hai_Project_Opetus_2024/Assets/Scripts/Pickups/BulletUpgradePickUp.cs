using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpgradePickUp : MonoBehaviour, IPickable
{
    public float speedBoost = 2f; // How much to increase bullet speed
    public float fireRateBoost = 0.1f; // How much to increase the fire rate
    public int damageBoost = 1; // How much to increase the damage
    
    public float maxDistanceFromPlayer = 20f; // Maximum distance from the player
    private Transform playerTransform;

    public void PickUp()
    {
        PlayerController player = playerTransform.gameObject.GetComponent<PlayerController>(); // Find the player in the scene
        if (player != null)
        {
            // Apply the upgrades
            Debug.LogError("FIX WEAPON UPGRADE");
            player.bulletData.speed += speedBoost;
            player.fireRate += fireRateBoost;
            player.bulletData.damage += damageBoost;

            Debug.Log("Weapon Upgraded!");
        }

        // Deactivate or destroy this power-up
        gameObject.SetActive(false); // Use this if you're pooling items
        // Destroy(gameObject); // Use this if you're not pooling
    }

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player
    }

    private void Update()
    {
        CheckDistanceAndDestroy();
    }

    private void CheckDistanceAndDestroy()
    {

        if (playerTransform == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > maxDistanceFromPlayer)
        {
            // If using pooling, return to pool instead of destroying
            // PoolManager.Instance.ReturnToPool(gameObject);
            Debug.Log("Destroy");
            Destroy(gameObject); // Destroy the object if it's too far from the player
        }
    }
}

