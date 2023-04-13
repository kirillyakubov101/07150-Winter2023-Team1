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
        }

        public override void Tick(float deltaTime)
        {
            if (this.m_unit.CurrentEnemy == null || this.m_unit.CurrentEnemy.IsDead())
            {
                LeaveToMoveState();
                return;
            }

            if(this.m_unit.CurrentEnemy is Unit)
            {
                Unit unit = (Unit)this.m_unit.CurrentEnemy;

                //check in range every "c_CheckInRangeCD" seconds to avoid too many calculations
                if (checkInRangeTimer >= c_CheckInRangeCD)
                {
                    checkInRangeTimer = 0f;

                    if (IsUnitOutOfRange(unit))
                    {
                        LeaveToMoveState();
                        return;
                    }
                }

                checkInRangeTimer += Time.deltaTime;
            }
        }

        private bool IsUnitOutOfRange(Unit target)
        {
            //float distance = Vector3.Distance(transform.position, this.m_unit.CurrentEnemy.transform.position);
            //float acceptable = this.m_unit.AttackRange + c_offsetRange;

            float distance = (target.transform.position - transform.position).magnitude;
            float acceptable = target.AttackRange + c_offsetRange;

            return distance > acceptable;
        }

        private void LeaveToMoveState()
        {
            this.m_unit.StateMachine.SwitchState(StateName.MOVE);
            this.m_unit.CurrentEnemy = null;
        }

        

        //anim event for melee units
        private void AttackAnimEvent()
        {
            if (this.m_unit.IsDead()) { return; }

            SoundManager.playSound?.Invoke(SoundManager.SoundType.SoldierAttack, transform.position);

            if (this.m_unit.CurrentEnemy == null || this.m_unit.CurrentEnemy.IsDead())
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

            SoundManager.playSound?.Invoke(SoundManager.SoundType.ArcherAttack, transform.position);

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

