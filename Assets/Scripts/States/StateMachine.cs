using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OurGame.Units;

namespace OurGame.State
{
    public class StateMachine : MonoBehaviour
    {
        [SerializeField] private State m_currentState;
        [SerializeField] private State[] m_states;
        [SerializeField] private Unit m_selfUnit;

        public Unit SelfUnit { get => m_selfUnit;  }

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

        public void SwitchState(State.StateName newState)
        {
            //
        }
    }

}

