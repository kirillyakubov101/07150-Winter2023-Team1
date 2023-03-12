using OurGame.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TowerDetectEnemy : MonoBehaviour
{
    #region Detection Variables

    public Vector3 collisionPoint = Vector3.zero;

    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private TowerTargeting towerTargeting;

    private GameObject hitObject;

    #endregion

    #region Unity Methods

    private void Start()
    {
        StartCoroutine("FindTargetWithDelay", 0.2f);
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    private void FindVisibleTargets()
    {
        towerTargeting.enemiesInRange.Clear();

        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position, transform.forward, towerTargeting.range, enemyLayer, QueryTriggerInteraction.Collide);

        if (hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hitObject = hits[i].collider.gameObject;
                collisionPoint = hits[i].point;

                print(hits[i].collider.name);

                if (hitObject.CompareTag("Enemy"))
                {
                    towerTargeting.enemiesInRange.Insert(i,hitObject.GetComponent<EnemyUnit>());
                }
                else
                if (hits[i].collider.GetComponent<EnemyUnit>().IsDead())
                {
                    towerTargeting.enemiesInRange.Remove(hits[i].collider.GetComponent<EnemyUnit>());
                    hitObject = null;
                }
                else
                {
                    hitObject = null;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collisionPoint, .5f);
    }

    #endregion
}
