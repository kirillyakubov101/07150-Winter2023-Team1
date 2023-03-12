using UnityEngine;

namespace OurGame.State
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State[] m_states; // |1 - Move | 2 - Attack| 3- Death|

        private State m_currentState;

        //private void Start()
        //{
        //    m_currentState = m_states[0]; //First state, MoveState
        //    m_currentState.EnterState();
        //}


        //-------------MAIN LOOP-----------
        private void Update()
        {
            if (m_currentState == null) { return; }

            m_currentState.Tick(Time.deltaTime);
        }
        //---------------------------------------

        private void OnEnable()
        {
            m_currentState = m_states[0];
            m_currentState.EnterState();
        }

        private void OnDisable()
        {
            m_currentState.ExitState();
            m_currentState = null;
        }

        public void SwitchState(State.StateName newState)
        {
            if (newState == m_currentState.GetStateName()) { return; } //avoid entering the same state twice

            if (m_currentState != null)
            {
                m_currentState.ExitState(); //leave the current state
            }

            switch (newState)
            {
                case State.StateName.MOVE:
                    m_currentState = m_states[0];   
                    break;
                case State.StateName.ATTACK:
                    m_currentState = m_states[1];
                    break;
                case State.StateName.DEATH:
                    m_currentState = m_states[2];
                    break;

                default:    
                    break;
            }

            m_currentState.EnterState();
        }
    }

}

