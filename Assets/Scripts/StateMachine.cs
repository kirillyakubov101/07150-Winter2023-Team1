using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OurGame.State
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State m_currentState;
        [SerializeField] private State[] m_states;

        private void Start()
        {
            InitStartingState();
        }


        //-------------MAIN LOOP-----------
        private void Update()
        {
            if (m_currentState == null) { return; }

            m_currentState.Tick(Time.deltaTime);
        }
        //---------------------------------------

        private void InitStartingState()
        {
            m_currentState = m_states[0]; //First state, MoveState
            m_currentState.EnterState();
        }
    }

}

