using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletUpgradePickUp : PickUp
{
    public float speedBoost = 2f; // How much to increase bullet speed
    public float fireRateBoost = 0.1f; // How much to increase the fire rate
    public int damageBoost = 1; // How much to increase the damage


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
    
}

