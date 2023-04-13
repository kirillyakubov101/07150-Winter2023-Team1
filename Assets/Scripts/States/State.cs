using OurGame.Units;
using UnityEngine;

namespace OurGame.State
{
    public abstract class State : MonoBehaviour
    {
        protected Unit m_unit;
        protected StateName m_currentStateName;
        
        public enum StateName
        {
            MOVE,
            ATTACK,
            DEATH
        }

        virtual protected void Awake()
        {
            m_unit = GetComponent<Unit>();
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

