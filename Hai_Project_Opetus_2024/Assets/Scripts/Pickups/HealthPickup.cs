using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IPickable
{
    public int healthRestoreAmount = 20;
    public float maxDistanceFromPlayer = 20f; // Maximum distance from the player
    private Transform playerTransform;

    public void PickUp()
    {
        PlayerController player = FindObjectOfType<PlayerController>(); // Find the player in the scene
        if (player != null)
        {
            player.RestoreHealth(healthRestoreAmount);
            Debug.Log("Health Restored!");
        }

        // Deactivate or destroy this pick-up
         // Use this if you're pooling items
         Destroy(gameObject); // Use this if you're not pooling
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
