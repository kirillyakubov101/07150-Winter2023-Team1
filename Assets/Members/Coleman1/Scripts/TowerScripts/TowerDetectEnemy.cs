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

    public Vector3[] collisionPoints; //The collision points detected in the raycast used to create the Gizmos an

    [Header("Game Objects")]

    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private A_Tower towerTargeting;

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

        //Raycast all will return all objects that it detects within it's range and inside the enemy layer and add them to the array of Raycast Hits to access info about the hits
        hits = Physics.RaycastAll(transform.position, transform.forward, towerTargeting.range, enemyLayer, QueryTriggerInteraction.Collide); 

        collisionPoints = new Vector3[hits.Count()];

        if (hits != null)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                hitObject = hits[i].collider.gameObject;
                collisionPoints[i] = hits[i].point;

                print(hits[i].collider.name);

                towerTargeting.enemiesInRange.Insert(i, hitObject.GetComponent<EnemyUnit>()); //Ad the enemy to the list of enemies for the Tower

                if (hits[i].collider.GetComponent<EnemyUnit>().IsDead())
                {
                    towerTargeting.enemiesInRange.Remove(hits[i].collider.GetComponent<EnemyUnit>()); //Removes the enemy from the list of enemies from the Tower
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

        foreach(Vector3 collider in collisionPoints)
        {
            Gizmos.DrawWireSphere(collider, .5f);
        }
    }

    #endregion
}
