using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName = "";
    [TextArea(3,10)]
    public string description = "";
    public float speed = 3f; // Movement speed of the enemy
    public int health = 10; // Health of the enemy
    public int scoreValue = 100;
    public GameObject dropPrefab; // The prefab that this enemy can drop upon death
    public float dropChance = 0.1f; // Chance to drop the item (0.1 for 10% chance)
}

