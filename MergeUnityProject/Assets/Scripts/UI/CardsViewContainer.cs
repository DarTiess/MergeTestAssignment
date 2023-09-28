using System;
using Card;
using DefaultNamespace;
using UnityEngine;

namespace UI
{
    public class CardsViewContainer: MonoBehaviour
    {
        [SerializeField] 
        private Transform container;
        [SerializeField] 
        private CardView cardViewPrefab; 
      
        private ICardSpawner _cardSpawner;
        private CardConfig[] _cardConfigs;

        public event Action SelecteCard;
        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="cardConfigs"></param>
        /// <param name="cardSpawner"></param>
        public void Init(CardConfig[] cardConfigs,ICardSpawner cardSpawner)
        {
            _cardConfigs = cardConfigs;
            _cardSpawner = cardSpawner;
            for (int i = 0; i < _cardConfigs.Length; i++)
            {
                AddItem(_cardConfigs[i]);
            }
        }
        /// <summary>
        /// Create cardView, and Initialize
        /// </summary>
        /// <param name="config"></param>
        private void AddItem(CardConfig config)
        {
            CardView cardView= Instantiate(cardViewPrefab, container);
            cardView.Initialize(config);
            cardView.SelectedCard += OnItemSelected;
        }
        /// <summary>
        /// If cardView was selected
        /// </summary>
        /// <param name="config"></param>
        private void OnItemSelected(CardConfig config)
        {
           _cardSpawner.SpawnCardOnRandomPosition(config);
           SelecteCard?.Invoke();
        }
    }
}