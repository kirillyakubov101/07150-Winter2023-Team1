using OurGame.Units;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonAI : MonoBehaviour
{
    private Vector3 projectileShootFromPosition;
    [SerializeField] public float range;
    [SerializeField] public float shootTimerMax;
    [SerializeField] public float shootTimer;
    [SerializeField] public float damage;

    public static List<EnemyUnit> enemyList = new List<EnemyUnit>();

    private void Start()
    {
        projectileShootFromPosition = transform.Find("ProjectileShootFromPosition").position;
        range = 100f;
        shootTimerMax = .05f;
        damage = 100f;
    }

    private void Update()
    {
        shootTimer -= Time.deltaTime;

        if(shootTimer <= 0f)
        {
            shootTimer = shootTimerMax;

            EnemyUnit enemy = GetClosestEnemy(projectileShootFromPosition, range);
            if (enemy != null)
            { //Valid Enemy
                ProjectileCannonBall.Create(projectileShootFromPosition, enemy, damage);
            }
        }
    }

    public static EnemyUnit GetClosestEnemy(Vector3 position, float maxRange)
    {
        EnemyUnit closestEnemy = null;
        foreach(EnemyUnit unit in enemyList) //Loop Through the list of enemies
        {
            if (unit.IsDead()) continue; //If the Unit is dead it will exit the loop
            if (Vector3.Distance(position, unit.transform.position) <= maxRange)
            {
                if(closestEnemy == null)
                {
                    closestEnemy = unit;
                }
                else
                if(Vector3.Distance(position, unit.transform.position) < Vector3.Distance(position, closestEnemy.transform.position))
                {
                    closestEnemy = unit;
                }
            }
        }
        return closestEnemy;
    }
}
