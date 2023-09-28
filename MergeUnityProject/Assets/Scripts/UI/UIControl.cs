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
        [SerializeField] 
        private StartMenu panelMenu;
        [SerializeField] 
        private GamePanel panelInGame;
        [SerializeField] 
        private WinPanel panelWin;  
        [SerializeField] 
        private LostPanel panelLost;
        [SerializeField] 
        private CardsViewContainer cardsViewContainer;
        [SerializeField] 
        private TaskViewContainer taskViewContainer;
            
        private ILevelManager _levelManager;
        private ILevelEvents _levelEvents;
        private ILevelLoader _levelLoader;
        private ICoinsUpdate _coinsUpdate;

        public event Action CardClicked;
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="levManager"></param>
        /// <param name="levelEvents"></param>
        /// <param name="levelLoader"></param>
        /// <param name="cardConfigs"></param>
        /// <param name="spawner"></param>
        /// <param name="coinsUpdate"></param>
        /// <param name="maxScaleSize"></param>
        /// <param name="animatedDuration"></param>
        public void Init(ILevelManager levManager, ILevelEvents levelEvents, 
                         ILevelLoader levelLoader, CardConfig[] cardConfigs, 
                         ICardSpawner spawner, ICoinsUpdate coinsUpdate,
                         float maxScaleSize,float animatedDuration)
        {
            _levelManager = levManager;
            _levelEvents = levelEvents;
            _levelLoader = levelLoader;
            _coinsUpdate = coinsUpdate;
     
            _levelEvents.OnLevelStart += OnLevelStart;
            _levelEvents.OnLateWin += OnLevelWin; 
            _levelEvents.OnLateLost += OnLevelLost;

            panelMenu.ClickedPanel += OnPlayGame;
            panelLost.ClickedPanel += RestartGame;
            panelInGame.ClickedPanel += RestartGame;
            panelWin.ClickedPanel += LoadNextLevel;
            _coinsUpdate.UpdateCoinsAmount += OnUpdateCoins;
            
            panelInGame.Initialize(maxScaleSize, animatedDuration);
            cardsViewContainer.Init(cardConfigs, spawner);
            cardsViewContainer.SelecteCard += OnSelectedCard;
            taskViewContainer.Initialize(cardConfigs, animatedDuration);
            taskViewContainer.AllTaskEnds += FinishedGame;
            
            OnLevelStart();
        }
        /// <summary>
        /// On disable clear
        /// </summary>
        private void OnDisable()
        {
            _levelEvents.OnLevelStart -= OnLevelStart;
            _levelEvents.OnLateWin -= OnLevelWin; 
            _levelEvents.OnLateLost -= OnLevelLost;

            panelMenu.ClickedPanel -= OnPlayGame;
            panelLost.ClickedPanel -= RestartGame;
            panelInGame.ClickedPanel -= RestartGame;
            panelWin.ClickedPanel -= LoadNextLevel;
            _coinsUpdate.UpdateCoinsAmount -= OnUpdateCoins;
            cardsViewContainer.SelecteCard -= OnSelectedCard;
        }
        /// <summary>
        /// On finished task, level is completed with win
        /// </summary>
        private void FinishedGame()
        {
            _levelManager.LevelWin();
        }
        /// <summary>
        /// On Upgrade coins, make upgrade UI
        /// and send taskView type of collected card
        /// </summary>
        /// <param name="price"></param>
        /// <param name="type"></param>
        private void OnUpdateCoins(int price, CardType type)
        {
            
            panelInGame.UpdateCoinsTextView(price);
            taskViewContainer.CollectCard(type);
        }
        /// <summary>
        /// On Selected card, change coins view
        /// </summary>
        private void OnSelectedCard()
        {
            CardClicked?.Invoke();
            panelInGame.UpdateCoinsTextView(_coinsUpdate.GetCoinsAmount());
        }
        /// <summary>
        /// On level start Show start panel
        /// </summary>
        private void OnLevelStart()      
        {   
            HideAllPanels();
            panelMenu.Show();
        }
        /// <summary>
        /// On win show win panel
        /// </summary>
        private void OnLevelWin()      
        {    
            Debug.Log("Level Win"); 
            HideAllPanels();
            panelWin.Show();  
        }
        /// <summary>
        /// On level lost show lost panel
        /// </summary>
        private void OnLevelLost()           
        {                                                     
            Debug.Log("Level Lost");  
            HideAllPanels();
            panelLost.Show();
        }
        /// <summary>
        /// On clicked pause button, send level manager to make event pause
        /// </summary>
        private void OnPauseGame()
        {
            _levelManager.PauseGame();
        }
        /// <summary>
        /// On play game, show game panel, and upgrade actual parameters
        /// </summary>
        private void OnPlayGame()
        { 
            _levelManager.PlayGame();
            HideAllPanels();
            panelInGame.UpdateCoinsTextView(_coinsUpdate.GetCoinsAmount());
            panelInGame.Show();         
        }
        /// <summary>
        /// Send to Load next level
        /// </summary>
        private void LoadNextLevel()
        {
            _levelLoader.LoadNextLevel();
        }
        /// <summary>
        /// Send to restart level
        /// </summary>
        private void RestartGame()
        {
            _levelLoader.RestartScene();
        }
        /// <summary>
        /// Hide all panels
        /// </summary>
        private void HideAllPanels()
        {
            panelMenu.Hide();
            panelLost.Hide();
            panelWin.Hide();
            panelInGame.Hide();
        }
    
    }
}
