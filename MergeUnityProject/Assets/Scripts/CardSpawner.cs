using System;
using Card;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CardSpawner: ICardSpawner, ICardUpgrade
    {
        private GridSpawner _gridSpawner;
        private Card.Card _cardPrefab;
        private ParticleSystem _explosionEffect;

        public event Action CardUpgrade;

        public CardSpawner(GridSpawner gridSpawner, Card.Card cardPrefab, ParticleSystem explosEffect)
        {
            _gridSpawner = gridSpawner;
            _cardPrefab = cardPrefab;
            _explosionEffect = explosEffect;
        }
        /// <summary>
       /// Spawn card on random position
       /// </summary>
       /// <param name="config">Cards configuration</param>
        public void SpawnCardOnRandomPosition(CardConfig config)
        {
            if (!_gridSpawner.HasEmptyGridPositions())
            {
                Debug.LogError("No empty grid positions!");
                return;
            }
        
            var gridSize = _gridSpawner.GridSize;

            var maxX = gridSize.x;
            var maxY = gridSize.y;
            
            Vector2Int randomGridPosition;
            
            do
            {
                var randomX = Random.Range(0, maxX);
                var randomY = Random.Range(0, maxY);
                
                randomGridPosition = new Vector2Int(randomX, randomY);
            } while (_gridSpawner.HasCardAtGridPosition(randomGridPosition));
            
            Card.Card newCard=  _gridSpawner.SpawnObjectAtGridPosition(_cardPrefab.gameObject, randomGridPosition).GetComponent<Card.Card>();
            newCard.Initialize(config, _explosionEffect);
            newCard.CardUpgrade += OnCardUpgrade;
        }

        private void OnCardUpgrade()
        {
            CardUpgrade?.Invoke();
        }
    }
}