using UnityEngine;

namespace OurGame.State
{
    public class AttackState : State
    {
        private readonly int MoveStateAnimHash = Animator.StringToHash("Attack");
        private const float CrossFadeDuration = 0.1f;

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
            if(this.m_unit.CurrentEnemy == null) // TODO: add a death condition in a form of .IsDead()
            {
                this.m_unit.StateMachine.SwitchState(StateName.MOVE);
            }
        }

        //anim evennt
        private void AttackAnimEvent()
        {
            print("attack");
        }
    }
}

