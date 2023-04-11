using OurGame.State;
using OurGame.Units;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class A_Tower : MonoBehaviour, IDamageable
{
    //Script needs to be a child component of any tower and requires an object with the TowerTargeting script component

    public enum TargetPriority
    {
        First,  //First in the list of enemies
        Strong, //Enemy with the most health
        Last    //Last enemy in the list
    }

    #region Tower variables

    [Header("Basic")]

    public float range;
    public float health;
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
    [SerializeField] private Transform towerPivot;

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
            towerPivot.LookAt(currentEnemy.transform);
            towerPivot.eulerAngles = new Vector3(towerPivot.eulerAngles.x, towerPivot.eulerAngles.y, 0);

            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPosition.position, Quaternion.identity);
            projectile.GetComponent<A_Projectile>().Initialize(currentEnemy, projectileDamage, projectileSpeed);
        }
    }

    public float GetRange()
    {
        return range;
    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
    }

    public bool IsDead()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
}
