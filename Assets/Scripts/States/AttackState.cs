using OurGame.Projectiles;
using OurGame.Units;
using UnityEngine;

namespace OurGame.State
{
    public class AttackState : State
    {
        private readonly int MoveStateAnimHash = Animator.StringToHash("Attack");
        private const float CrossFadeDuration = 0.1f;

        private float checkInRangeTimer = 0f;
        private const float c_CheckInRangeCD = 1f;

        [Header("ONLY FOR RANGED UNITS")][Tooltip("only for ranged units | keep null if melee")]
        [SerializeField] private Projectile m_projectilePrefab;
        [SerializeField] private Transform m_shootingPoint;

        //this is the offset to add to the attack range when checking "OutOfRange" functionality
        private const float c_offsetRange = 3f;

        private Unit m_targetUnit;

        private AttackState()
        {
            this.m_currentStateName = StateName.ATTACK;
        }

        public override void EnterState()
        {
            this.m_unit.Animator.CrossFadeInFixedTime(MoveStateAnimHash, CrossFadeDuration);
        }

        public override void ExitState()
        {
           this.m_unit.CurrentEnemy = null;
           this.m_targetUnit = null;
        }

        public override void Tick(float deltaTime)
        {
            if(this.m_unit.CurrentEnemy == null || this.m_unit.CurrentEnemy.IsDead())
            {
                LeaveToMoveState();
                return;
            }

            if(this.m_unit.CurrentEnemy is Unit && m_targetUnit == null)
            {
                m_targetUnit = (Unit)this.m_unit.CurrentEnemy;
            }
          
            if(m_targetUnit != null)
            {
                checkInRangeTimer += Time.deltaTime;
                //check in range every "c_CheckInRangeCD" seconds to avoid too many calculations
                if (checkInRangeTimer >= c_CheckInRangeCD)
                {
                    checkInRangeTimer = 0f;

                    if (IsUnitOutOfRange())
                    {
                        LeaveToMoveState();
                    }
                }
            }
                
        }

        private bool IsUnitOutOfRange()
        {
            float distance = Vector3.Distance(transform.position, m_targetUnit.transform.position);
            float acceptable = this.m_unit.AttackRange + c_offsetRange;

            //float distance = (m_targetUnit.transform.position - transform.position).magnitude;
            //float acceptable = m_targetUnit.AttackRange + c_offsetRange;

            return distance > acceptable;
        }

        private void LeaveToMoveState()
        {
            this.m_unit.StateMachine.SwitchState(StateName.MOVE);
        }

        

        //anim event for melee units
        private void AttackAnimEvent()
        {
            if (this.m_unit.IsDead()) { return; }
            if(this.m_unit.CurrentEnemy == null || this.m_unit.CurrentEnemy.IsDead())
            {
                LeaveToMoveState();
                return;
            }

            this.m_unit.CurrentEnemy.TakeDamage(this.m_unit.UnitDamage);

        }

        //anim event for ranged units
        private void LaunchProjectile()
        {
            if(m_projectilePrefab == null) { return; }
            if (this.m_unit.IsDead()) { return; }

            if (this.m_unit.CurrentEnemy == null || this.m_unit.CurrentEnemy.IsDead())
            {
                LeaveToMoveState();
                return;
            }

            //Launch
            var inst = Instantiate(m_projectilePrefab, m_shootingPoint);
            inst.InitStats(this.m_unit.UnitDamage);
        }
    }
}

