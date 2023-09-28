using System;
using Card;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class CardSpawner: ICardSpawner
    {
        private GridBuilder _cardSpawner;
        private ICoinsUpdate _coins;
        private ICoinsAnimationView _coinsAnimation;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="cardSpawner"></param>
        /// <param name="coins"></param>
        public CardSpawner(GridBuilder cardSpawner, ICoinsUpdate coins, ICoinsAnimationView coinsAnimation)
        {
            _cardSpawner = cardSpawner;
            _coins = coins;
            _coinsAnimation = coinsAnimation;
            _cardSpawner.FillGrid();
           
        }
        /// <summary>
       /// Spawn card on random position
       /// </summary>
       /// <param name="config">Cards configuration</param>
        public void SpawnCardOnRandomPosition(CardConfig config)
        {
            if (!_cardSpawner.HasEmptyGridPositions())
            {
                Debug.LogError("No empty grid positions!");
                return;
            }
            
            if (_coins.GetCoinsAmount() <=0)
            {
                Debug.LogError("You haven't coins!");
                return;
            }
        
            var gridSize = _cardSpawner.GridSize;

            var maxX = gridSize.x;
            var maxY = gridSize.y;
            
            Vector2Int randomGridPosition;
            
            do
            {
                var randomX = Random.Range(0, maxX);
                var randomY = Random.Range(0, maxY);
                
                randomGridPosition = new Vector2Int(randomX, randomY);
            } while (_cardSpawner.HasCardAtGridPosition(randomGridPosition));
            
            
            _cardSpawner.SpawnObjectAtFreePosition(randomGridPosition, config);
            _coins.SpendCoins(1);
            _coinsAnimation.SpendCoinsAnimation();
        }

       
    }
}