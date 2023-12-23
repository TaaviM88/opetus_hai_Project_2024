using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewBulletData", menuName = "Bullet Data")]
public class BulletData : ScriptableObject
{
    public float speed = 10f;
    public int damage = 1;
    public Sprite bulletSprite;
   
}
