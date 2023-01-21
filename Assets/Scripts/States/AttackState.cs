using UnityEngine;

namespace OurGame.State
{
    public class AttackState : State
    {
        private AttackState()
        {
            this.m_currentStateName = StateName.ATTACK;
        }

        public override void EnterState()
        {
            print("entered attack state");
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
    }
}

