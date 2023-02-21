using UnityEngine;
namespace OurGame.State
{
    public class DeathState : State
    {
        private readonly int MoveStateAnimHash = Animator.StringToHash("Death");
        private const float CrossFadeDuration = 0.1f;

        [SerializeField] private Collider m_collider;

        private DeathState()
        {
            this.m_currentStateName = StateName.DEATH;
        }

        public override void EnterState()
        {
            if(this.m_unit)
            {
                this.m_unit.Animator.CrossFadeInFixedTime(MoveStateAnimHash, CrossFadeDuration);
            }

            m_collider.enabled = false;

        }

        public override void ExitState()
        {
            m_collider.enabled = true;
        }

        public override void Tick(float deltaTime)
        {
            //
        }

    }
}

