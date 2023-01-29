using UnityEngine;

namespace OurGame.State
{
    public class MoveState : State
    {
        private readonly int MoveStateAnimHash = Animator.StringToHash("Move");
        private const float CrossFadeDuration = 0.1f;

        private MoveState()
        {
            this.m_currentStateName = StateName.MOVE;
        }

        public override void EnterState()
        {
           this.m_unit.Animator.CrossFadeInFixedTime(MoveStateAnimHash, CrossFadeDuration);
        }

        public override void ExitState()
        {
           //
        }

        public override void Tick(float deltaTime)
        {
            transform.Translate(transform.forward * deltaTime * this.m_unit.MoveSpeed, Space.World);
        }

        
    }
}

