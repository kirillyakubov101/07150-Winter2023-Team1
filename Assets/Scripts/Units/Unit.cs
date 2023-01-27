using UnityEngine;
using OurGame.State;

namespace OurGame.Units
{
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] private string m_unitName;                              //TODO: USE SCRIPTABLE OBJECTS
        [SerializeField] private float m_unitDamage;                             //unit damage
        [SerializeField] private float m_moveSpeed = 10f;                        //unit move speed
        [SerializeField] private float m_attackRange = 5f;                       //unit attack range
        [SerializeField] private LayerMask m_enemyLayerMask = new LayerMask();   //the layer it looks for as opponent
        [SerializeField] private StateMachine m_stateMachine;                    //the state machine to change states
        [SerializeField] private Transform m_orientation;                        //the transform to raycast from
        [SerializeField] private Unit m_currentEnemy;                            //TODO: remove serialize
        [field:SerializeField] public Animator Animator { get; private set; }

        public float MoveSpeed { get => m_moveSpeed; }
        public Unit CurrentEnemy { get => m_currentEnemy; set => m_currentEnemy = value; }
        public StateMachine StateMachine { get => m_stateMachine; }

        private void FixedUpdate()
        {
            if (m_currentEnemy != null) { return; } //if we have an enemy/foe we don't search for a new one
            CheckForEnemy();
        }
        private void CheckForEnemy()
        {
            bool hasHit = Physics.Raycast(m_orientation.position, m_orientation.forward,out RaycastHit hitInfo, m_attackRange, m_enemyLayerMask);
           
            if (hasHit)
            {
                if(hitInfo.transform.TryGetComponent(out Unit unit))
                {
                    m_currentEnemy = unit;
                    m_stateMachine.SwitchState(State.State.StateName.ATTACK);
                }
            }
        }
    }
}

