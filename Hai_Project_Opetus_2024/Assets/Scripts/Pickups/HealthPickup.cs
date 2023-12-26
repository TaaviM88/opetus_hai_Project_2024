using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : PickUp, IPickable
{
    public int healthRestoreAmount = 20;
    
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

}
