using OurGame.State;
using OurGame.Units;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TowerTargeting : MonoBehaviour
{

    //Script needs to be a child component of any tower and requires an object with the TowerTargeting script component

    public enum TargetPriority
    {
        First,
        Close, //We calculate the magnitude to find the closest enemy in the list of enemies
        Strong,
        Last
    }

    #region Tower variables

    [Header("Basic")]

    [SerializeField] public float range;
    [SerializeField] private TargetPriority priority;

    public List<EnemyUnit> enemiesInRange = new List<EnemyUnit>();
    private EnemyUnit currentEnemy;
    public bool rotateTowardsTarget; 

    [Header("Attacking")]

    [SerializeField] private float attackSpeed;
    [SerializeField] private int projectileDamage;
    [SerializeField] private float projectileSpeed;

    private Quaternion defaultRotation;

    [Header("Game Objects")]

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPosition;

    private float lastAttackTimer;

    #endregion

    #region Unity Methods

    private void Update()
    {
        // attack every "attackRate" seconds
        if (Time.time - lastAttackTimer > attackSpeed)
        {
            lastAttackTimer = Time.time;
            currentEnemy = GetCurrentEnemy();
            if (currentEnemy != null)
                Attack();
        }
    }

    private void Start()
    {
        defaultRotation = this.transform.rotation;
    }

    #endregion

    #region Tower Methods

    //return the enemy that the tower will attack
    private EnemyUnit GetCurrentEnemy() 
    { 
        enemiesInRange.RemoveAll(x => x == null); //lambda expression to predicate the condition to remove all for the list (Here is if x == null)

        if(enemiesInRange.Count == 0)
        {
            this.transform.rotation = defaultRotation; //resets the rotation of the turret pivot

            return null; //if there are no enemies in the list it returns null
        }
         
        if(enemiesInRange.Count == 1) return enemiesInRange[0]; //If there is only one enemy in range it will return that enemy

        switch(priority)
        {
            case TargetPriority.First: return enemiesInRange.First(); //targets the first enemy inside the list

            case TargetPriority.Close:

                EnemyUnit closest = null;
                float distance = range; //the maximum distance

                for(int i = 0; i < enemiesInRange.Count; i++)
                {
                    float dist = (this.transform.position - enemiesInRange[i].transform.position).sqrMagnitude;

                    if(dist > distance)
                    {
                        closest = enemiesInRange[i];
                        distance = dist;
                    }
                }

                return closest;

                //REQUIRES GETTER FOR HEALTH

            //case TargetPriority.Strong:
                
            //    EnemyUnit strongest = null;
            //    int strongestHealth = 0;
            //    foreach (EnemyUnit enemy in enemiesInRange)
            //    {
            //        if (enemy.Health > strongestHealth)
            //        {
            //            strongest = enemy;
            //            strongestHealth = enemy.Health;
            //        }
            //    }
            //    return strongest;
                

            case TargetPriority.Last: return enemiesInRange.Last(); //targets last enemy in the list

            default: return null;
        }
    }

    private void Attack()
    {
        if (rotateTowardsTarget)
        {
            transform.LookAt(currentEnemy.transform);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Initialize(currentEnemy, projectileDamage, projectileSpeed);
        }
    }

    public float GetRange()
    {
        return range;
    }

    #endregion
}
