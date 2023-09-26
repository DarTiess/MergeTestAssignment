using System;
using Card;
using DefaultNamespace;
using Infrastructure.Level;
using UI.UIPanels;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class UIControl : MonoBehaviour, IUIClicked
    {
        [Header("Panels")]
        [SerializeField] private StartMenu panelMenu;
        [SerializeField] private GamePanel panelInGame;
        [SerializeField] private WinPanel panelWin;  
        [SerializeField] private LostPanel panelLost;
        [SerializeField] private CardsViewContainer cardsViewContainer;
            
        private ILevelManager _levelManager;
        private ILevelEvents _levelEvents;
        private ILevelLoader _levelLoader;

        public event Action CardClicked;  

        public void Init(ILevelManager levManager, ILevelEvents levelEvents, ILevelLoader levelLoader, CardConfig[] cardConfigs, ICardSpawner spawner)
        {
            _levelManager = levManager;
            _levelEvents = levelEvents;
            _levelLoader = levelLoader;
     
            _levelEvents.OnLevelStart += OnLevelStart;
            _levelEvents.OnLateWin += OnLevelWin; 
            _levelEvents.OnLateLost += OnLevelLost;

            panelMenu.ClickedPanel += OnPlayGame;
            panelLost.ClickedPanel += RestartGame;
            panelInGame.ClickedPanel += OnPauseGame;
            panelWin.ClickedPanel += LoadNextLevel;
            cardsViewContainer.Init(cardConfigs, spawner);
            cardsViewContainer.SelecteCard += OnSelectedCard;
            
            OnLevelStart();
        }

        private void OnSelectedCard()
        {
            CardClicked?.Invoke();
        }

        private void OnDisable()
        {
            _levelEvents.OnLevelStart -= OnLevelStart;
            _levelEvents.OnLateWin -= OnLevelWin; 
            _levelEvents.OnLateLost -= OnLevelLost;

            panelMenu.ClickedPanel -= OnPlayGame;
            panelLost.ClickedPanel -= RestartGame;
            panelInGame.ClickedPanel -= OnPauseGame;
            panelWin.ClickedPanel -= LoadNextLevel;
        }

        private void OnLevelStart()      
        {   
            HideAllPanels();
            panelMenu.Show();
        }
                             
        private void OnLevelWin()      
        {    
            Debug.Log("Level Win"); 
            HideAllPanels();
            panelWin.Show();  
        }

        private void OnLevelLost()           
        {                                                     
            Debug.Log("Level Lost");  
            HideAllPanels();
            panelLost.Show();
        }
        private void OnPauseGame()
        {
            _levelManager.PauseGame();
        }

        private void OnPlayGame()
        { 
            _levelManager.PlayGame();
            HideAllPanels(); 
            panelInGame.Show();         
        }
        private void LoadNextLevel()
        {
            _levelLoader.LoadNextLevel();
        }

        private void RestartGame()
        {
            _levelLoader.RestartScene();
        }

        private void HideAllPanels()
        {
            panelMenu.Hide();
            panelLost.Hide();
            panelWin.Hide();
            panelInGame.Hide();
        }
    
    }
}
