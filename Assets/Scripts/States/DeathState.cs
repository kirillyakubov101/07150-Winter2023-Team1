using UnityEngine;

namespace OurGame.State
{
    public class DeathState : State
    {
        private readonly int MoveStateAnimHash = Animator.StringToHash("Death");
        private const float CrossFadeDuration = 0.1f;

        private DeathState()
        {
            this.m_currentStateName = StateName.DEATH;
        }

        public override void EnterState()
        {
            this.m_unit.Animator.CrossFadeInFixedTime(MoveStateAnimHash, CrossFadeDuration);
            this.m_unit.CurrentEnemy = null;
            Destroy(this.m_unit);
        }

        public override void ExitState()
        {
            //
        }

        public override void Tick(float deltaTime)
        {
            //
        }
    }
}

