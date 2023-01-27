using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame.State
{
    public class DeathState : State
    {
        private DeathState()
        {
            this.m_currentStateName = StateName.DEATH;
        }

        public override void EnterState()
        {
            //
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

