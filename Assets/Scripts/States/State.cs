using OurGame.Units;
using UnityEngine;

namespace OurGame.State
{
    public abstract class State : MonoBehaviour
    {
        [SerializeField] protected Unit m_unit;
        protected StateName m_currentStateName;
        
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

