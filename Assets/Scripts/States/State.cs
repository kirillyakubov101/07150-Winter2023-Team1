using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame.State
{
    public abstract class State : MonoBehaviour
    {
        protected StateMachine m_machine;
        protected StateName m_currentStateName;
        
        protected virtual void Awake()
        {
            m_machine = GetComponent<StateMachine>();
        }

        public enum StateName
        {
            MOVE,
            ATTACK,
            DEATH
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void Tick(float deltaTime);
    }
}

