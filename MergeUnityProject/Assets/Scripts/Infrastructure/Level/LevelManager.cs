using System;
using UnityEngine;

namespace Infrastructure.Level
{
    public class LevelManager : ILevelManager, ILevelEvents
    {
        private float _timeWaitLose;
        private float _timeWaitWin;
        private bool _onPaused;
        
        public event Action OnLevelStart;
        public event Action OnLevelWin;
        public event Action OnLateWin;
        public event Action OnLevelLost;
        public event Action OnLateLost;
        public event Action OnPlayGame;
        public event Action StopGame;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="timeWaitLose"></param>
        /// <param name="timeWaitWin"></param>
        public LevelManager(float timeWaitLose, float timeWaitWin)
        {
            _timeWaitLose = timeWaitLose;
            _timeWaitWin = timeWaitWin;
            LevelStart();
        }

        /// <summary>
        /// After timer invoke Action LateLost
        /// </summary>
        private void LateLost()
        {
            while (_timeWaitLose>0)
            {
                _timeWaitLose -= Time.deltaTime;
            }
            OnLateLost?.Invoke();
        }

        /// <summary>
        /// After timer invoke Action LateLost
        /// </summary>
        private void LateWin()
        {
            while (_timeWaitWin>0)
            {
                _timeWaitWin -= Time.deltaTime;
            }
            OnLateWin?.Invoke();
        }

        /// <summary>
        /// Level start
        /// invoke Action
        /// </summary>
        public void LevelStart()
        {
            OnLevelStart?.Invoke();
        }

        /// <summary>
        /// On pause game
        /// invoke Action
        /// </summary>
        public void PauseGame()
        {
            if (!_onPaused)
            {
                StopGame?.Invoke();
                _onPaused = true;
            }
            else
            {
                PlayGame();
                _onPaused = false;
            }
        }

        /// <summary>
        /// On play game
        /// invoke Action
        /// </summary>
        public void PlayGame()
        {
            OnPlayGame?.Invoke();
        }

        /// <summary>
        /// On level lost game
        /// invoke Action
        /// </summary>
        public void LevelLost()
        {
            OnLevelLost?.Invoke();
            LateLost();
        }

        /// <summary>
        /// On win game
        /// invoke Action
        /// </summary>
        public void LevelWin()
        {
            OnLevelWin?.Invoke();
            LateWin();
        }
    }
}
