using System;
using UnityEngine;

namespace Infrastructure.Level
{
    public class LevelManager : ILevelManager, ILevelEvents
    {
        public event Action OnLevelStart;
        public event Action OnLevelWin;
        public event Action OnLateWin;
        public event Action OnLevelLost;
        public event Action OnLateLost;
        public event Action OnPlayGame;
        public event Action StopGame;
        
        private float timeWaitLose;
        private float timeWaitWin;
        private bool _onPaused;
        public LevelManager(float timeWaitLose, float timeWaitWin)
        {
            this.timeWaitLose = timeWaitLose;
            this.timeWaitWin = timeWaitWin;
            LevelStart();
        }
    
        public void LevelStart()
        {
            OnLevelStart?.Invoke();
        }

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

        public void PlayGame()
        {
            OnPlayGame?.Invoke();
        }

        public void LevelLost()
        {
            OnLevelLost?.Invoke();
            LateLost();
        }

        private void LateLost()
        {
            while (timeWaitLose>0)
            {
                timeWaitLose -= Time.deltaTime;
            }
            OnLateLost?.Invoke();
        }

        public void LevelWin()
        {
            OnLevelWin?.Invoke();
            LateWin();
        }

        private void LateWin()
        {
            while (timeWaitWin>0)
            {
                timeWaitWin -= Time.deltaTime;
            }
            OnLateWin?.Invoke();
        }

    }
}
