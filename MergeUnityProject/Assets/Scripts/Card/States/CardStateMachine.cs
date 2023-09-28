using System;
using System.Collections.Generic;
using UnityEngine;

namespace Card.States
{
    public class CardStateMachine
    {
        private State _currentState;
        private Dictionary<Type, State> _states = new Dictionary<Type, State>();

        /// <summary>
        /// Fill state list
        /// </summary>
        /// <param name="state"></param>
        public void AddState(State state)
        {
            _states.Add(state.GetType(), state);
        }
        /// <summary>
        /// set current state
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>icon of state</returns>
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
        /// <summary>
        /// Change current state from current
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>icon of next state</returns>
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
        /// <summary>
        /// Get current state
        /// </summary>
        /// <returns>state</returns>
        public State GetCurrentState()
        {
            return _currentState;
        }
    }
}