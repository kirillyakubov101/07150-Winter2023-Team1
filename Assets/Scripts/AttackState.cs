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
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public override void Tick(float deltaTime)
        {
            throw new System.NotImplementedException();
        }
    }
}

