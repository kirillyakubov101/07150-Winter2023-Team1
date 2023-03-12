using OurGame.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class A_Projectile : MonoBehaviour
{

    //Script is required to be a component of any projectile prefab fired from any tower

    #region Projectile Variables

    [Header("Projectile Info")]

    [SerializeField] private int damage;
    [SerializeField] private float moveSpeed;

    private EnemyUnit target;

    [Header("Projectile Prefab")]

    [SerializeField] public GameObject hitSpawnPrefab;

    public void Initialize(EnemyUnit target, int damage, float moveSpeed)
    {
        this.target = target;
        this.damage = damage;
        this.moveSpeed = moveSpeed;
    }

    #endregion

    #region Unity Methods

    private void Update()
    {
        if(target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);

            transform.LookAt(target.transform);

            if (Vector3.Distance(transform.position, target.transform.position) < 5f)
            {
                target.TakeDamage(damage);

                if(hitSpawnPrefab != null)
                {
                    Instantiate(hitSpawnPrefab, transform.position, Quaternion.identity);
                }

                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
