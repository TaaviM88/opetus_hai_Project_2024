using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletUpgradeLevel
{
    public float additionalSpeed = 0;
    public int additionalDamage = 0;
}

[System.Serializable]
public class UpgradedBullet
{
    public float currenSpeed = 0;
    public int currentDamage = 0;
}


[CreateAssetMenu(fileName = "NewBulletData", menuName = "Bullet Data")]
public class BulletData : ScriptableObject
{
    public float speed = 10f;
    public int damage = 1;
    public List<BulletUpgradeLevel> upgrades;
    public Sprite bulletSprite;
   
    public UpgradedBullet GetBullet(int upgradeLevel)
    {
        UpgradedBullet bullet = new UpgradedBullet();

        if(upgradeLevel >=  upgrades.Count)
        {
            upgradeLevel = upgrades.Count - 1;
        }
 
        bullet.currenSpeed = speed + upgrades[upgradeLevel].additionalSpeed;
        bullet.currentDamage = damage + upgrades[upgradeLevel].additionalDamage;

        return bullet;
    }
}
