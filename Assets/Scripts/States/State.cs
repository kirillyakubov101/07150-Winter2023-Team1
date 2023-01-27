using OurGame.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame.State
{
    public abstract class State : MonoBehaviour
    {
        protected Unit m_unit;
        protected StateName m_currentStateName;
        
        protected virtual void Awake()
        {
            m_unit = GetComponent<Unit>();
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

        public StateName GetStateName()
        {
            return m_currentStateName;
        }
    }
}

