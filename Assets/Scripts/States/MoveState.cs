using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame.State
{
    public class MoveState : State
    {
        private MoveState()
        {
            this.m_currentStateName = StateName.MOVE;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Move State");
        }

        public override void ExitState()
        {
            Debug.Log("Exit Move State");
        }

        public override void Tick(float deltaTime)
        {
            transform.Translate(transform.forward * deltaTime * this.m_machine.SelfUnit.MoveSpeed);
        }
    }
}

