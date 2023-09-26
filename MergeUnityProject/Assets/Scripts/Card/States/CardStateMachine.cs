using System;
using System.Collections.Generic;
using UnityEngine;

namespace Card.States
{
    public class CardStateMachine
    {
        private State _currentState;
        private Dictionary<Type, State> _states = new Dictionary<Type, State>();

        public void AddState(State state)
        {
            _states.Add(state.GetType(), state);
        }

        public Sprite SetState<T>() where T : State
        {
            Sprite icon = null;
            var type = typeof(T);
            if (_currentState!=null &&_currentState.GetType() == type)
            {
                return null;
            }

            if (_states.TryGetValue(type, out var newstate))
            {
                _currentState?.Exit();
                _currentState = newstate;
              icon= _currentState.Enter();
            }

            return icon;
        }

        public Sprite ChangeState<T>() where T:State
        {
            Sprite icon = null;
            var type = typeof(T);
            if (_states.TryGetValue(type, out var newstate))
            {
                
                _currentState = newstate;
             icon =_currentState.Enter();
            }
            return icon;
        }

        public State GetCurrentState()
        {
            return _currentState;
        }
    }
}