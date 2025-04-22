using System;
using System.Collections.Generic;
using UnityEngine;

namespace RogueLike.Scripts.StateMachine
{
    public abstract class StateManager<EState>: MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
        
        protected BaseState<EState> CurrentState;
        
        protected bool IsTransitioningState = false;
        
        private void Start()
        {
            CurrentState.EnterState();
        }

        private void Update()
        {
            EState nextStateKey = CurrentState.GetNextState();

            if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
            {
                CurrentState.UpdateState();
            }
            else if (!IsTransitioningState)
            {
                TransitionToState(nextStateKey);
            }
        }

        public void TransitionToState(EState stateKey)
        {
            IsTransitioningState = true;
            CurrentState.ExitState();
            CurrentState = States[stateKey];
            CurrentState.EnterState();
            IsTransitioningState = false;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            CurrentState.OnTriggerEnter2D(other);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            CurrentState.OnTriggerStay2D(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            CurrentState.OnTriggerExit2D(other);
        }
    }
}