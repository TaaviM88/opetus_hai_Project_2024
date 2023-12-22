using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;
    public bool useSprite = false;
    private float lifespan = 5f; // How long the bullet should live in seconds
    private float lifeTimer; // The countdown timer
    private void Start()
    {
        if(useSprite)
        {
            GetComponent<SpriteRenderer>().sprite = bulletData.bulletSprite;
        }
    }

    private void OnEnable()
    {
        lifeTimer = lifespan;
    }

    void Update()
    {
        // Move the bullet forward
        if (bulletData != null)
        {
            transform.Translate(Vector3.up *  -1 * bulletData.speed * Time.deltaTime);
        }


        // Count down the life timer
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            // Time's up, return the bullet to the pool
            BulletPoolManager.Instance.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletPoolManager.Instance.ReturnBullet(gameObject);
    }
}
