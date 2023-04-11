using UnityEngine;
using OurGame.State;
using System;
using OurGame.UI;

namespace OurGame.Units
{
    public abstract class Unit : MonoBehaviour, IDamageable
    {
        [SerializeField] private float m_unitDamage;                             //unit damage
        [SerializeField] private float m_moveSpeed = 10f;                        //unit move speed
        [SerializeField] private float m_attackRange = 5f;                       //unit attack range
        [SerializeField] private float m_maxHealth;
        [SerializeField] private float m_health;
        [SerializeField] private LayerMask m_enemyLayerMask = new LayerMask();   //the layer it looks for as opponent
        [SerializeField] private StateMachine m_stateMachine;                    //the state machine to change states
        [SerializeField] private Transform m_orientation;                        //the transform to raycast from
        [SerializeField] protected Transform m_canvasSpace;

        [field:SerializeField] public Animator Animator { get; private set; }


        private IDamageable m_currentEnemy;

        public float MoveSpeed { get => m_moveSpeed; }
        public IDamageable CurrentEnemy { get => m_currentEnemy; set => m_currentEnemy = value; }
        public StateMachine StateMachine { get => m_stateMachine; }
        public float UnitDamage { get => m_unitDamage; }
        public float AttackRange { get => m_attackRange;}

        //Take Damage Delegate / Event
        public event Action<float> OnTakeDamage;

        public virtual void OnEnable()
        {
            this.m_health = this.m_maxHealth; //init
            m_currentEnemy = null;
        }

        private void FixedUpdate()
        {
            if (m_currentEnemy != null || this.IsDead()) { return; } //if we have an enemy/foe we don't search for a new one
            CheckForEnemy();
        }
        private void CheckForEnemy()
        {
            bool hasHit = Physics.Raycast(m_orientation.position, m_orientation.forward,out RaycastHit hitInfo, m_attackRange, m_enemyLayerMask);
           
            if (hasHit)
            {
                if(hitInfo.transform.TryGetComponent(out IDamageable damageable) && !damageable.IsDead())
                {
                    m_currentEnemy = damageable;
                    m_stateMachine.SwitchState(State.State.StateName.ATTACK);
                }
            }
        }

        public virtual void TakeDamage(float damage)
        {
            if (IsDead()) { return; }
            this.m_health = Mathf.Max(this.m_health - damage, 0f);

            OnTakeDamage?.Invoke(GetHealthPercent());
            TextFactory.Instance.CreateDamageText(damage, m_canvasSpace);

            if (IsDead())
            {
                StateMachine.SwitchState(State.State.StateName.DEATH);
            }
        }


        public bool IsDead()
        {
            return this.m_health <= 0f;
        }

        private float GetHealthPercent()
        {
            return this.m_health / this.m_maxHealth;
        }
    }
}

