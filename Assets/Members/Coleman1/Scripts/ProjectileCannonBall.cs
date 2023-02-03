using OurGame.Units;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileCannonBall : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 20f;
    [SerializeField] public float destroyBallDistance = 1f;

    public static void Create(Vector3 spawnPosition, EnemyUnit enemy, float damage)
    {
        Transform ballTransform = Instantiate(GameAssetsRef.Instance.pfCannonBall, spawnPosition, Quaternion.identity);

        ProjectileCannonBall projectile = ballTransform.GetComponent<ProjectileCannonBall>();
        projectile.Setup(enemy, damage);
    }

    private EnemyUnit enemy;
    private float damage;

    private void Setup(EnemyUnit enemy, float damage)
    {
        this.enemy = enemy;
        this.damage = damage;
    }

    private void Update()
    {
        Vector3 targetPosition = enemy.transform.position;
        Vector3 moveDir = (targetPosition - transform.position).normalized;

        transform.position += moveDir * moveSpeed * Time.deltaTime;

        if(Vector3.Distance(transform.position, targetPosition) < destroyBallDistance )
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
