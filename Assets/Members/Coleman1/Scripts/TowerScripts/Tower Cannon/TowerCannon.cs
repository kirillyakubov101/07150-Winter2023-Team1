using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class TowerCannon : A_Tower
{
    public override void Attack()
    {
        if (rotateTowardsTarget)
        {
            towerPivot.LookAt(currentEnemy.transform);
            towerPivot.eulerAngles = new Vector3(towerPivot.eulerAngles.x, towerPivot.eulerAngles.y, 0);

            SoundManager.playSound?.Invoke(SoundManager.SoundType.CannonAttack, transform.position);

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, Quaternion.identity);
            projectile.GetComponent<A_Projectile>().Initialize(currentEnemy, projectileDamage, projectileSpeed);
        }
    }
}
