using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboMultiplierPickable : MonoBehaviour, IPickable
{
    public int multiplierIncrease = 1; // The amount to increase the combo multiplier
    public float maxDistanceFromPlayer = 20f; // Maximum distance from the player
    private Transform playerTransform;

    public void PickUp()
    {
        // Increase the player's combo multiplier
        UIManager.Instance.IncreaseComboMultiplier(multiplierIncrease);

        // Destroy or deactivate the pickable object
        Destroy(gameObject);
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