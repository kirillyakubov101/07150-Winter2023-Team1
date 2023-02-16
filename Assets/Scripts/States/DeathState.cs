using UnityEngine;
using OurGame.Units;
using OurGame.Spawn;

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
            if(this.m_unit)
            {
                this.m_unit.Animator.CrossFadeInFixedTime(MoveStateAnimHash, CrossFadeDuration);


                //if friendly do simple death
                //if enemy, pooling death system
                if (this.m_unit is EnemyUnit)
                {
                    Invoke(nameof(DelayRemoval),3f);
                }

            }

        }

        public override void ExitState()
        {
            //
        }

        public override void Tick(float deltaTime)
        {
            //
        }

        private void DelayRemoval()
        {
            gameObject.SetActive(false);
        }
    }
}

