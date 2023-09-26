using System;
using Card;
using DefaultNamespace;
using UnityEngine;

namespace UI
{
    public class CardsViewContainer: MonoBehaviour
    {
        [SerializeField] private Transform container;
        [SerializeField] private CardView cardViewPrefab; 
      
        private ICardSpawner _cardSpawner;
        private CardConfig[] _cardConfigs;

        public event Action SelecteCard;
        public void Init(CardConfig[] cardConfigs,ICardSpawner cardSpawner)
        {
            _cardConfigs = cardConfigs;
            _cardSpawner = cardSpawner;
            for (int i = 0; i < _cardConfigs.Length; i++)
            {
                AddItem(_cardConfigs[i]);
            }
        }

        private void AddItem(CardConfig config)
        {
            CardView cardView= Instantiate(cardViewPrefab, container);
            cardView.Initialize(config);
            cardView.SelectedCard += OnItemSelected;
        }
        
        private void OnItemSelected(CardConfig config)
        {
           _cardSpawner.SpawnCardOnRandomPosition(config);
           SelecteCard?.Invoke();
        }
    }
}