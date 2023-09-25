using Card;
using UI;
using UnityEngine;

namespace DefaultNamespace
{
    public class CardSpawner: MonoBehaviour
    {
        [SerializeField] private GridSpawner gridSpawner;
        
        [SerializeField] private Card.Card cardPrefab;
       /// <summary>
       /// Spawn card on random position
       /// </summary>
       /// <param name="config">Cards configuration</param>
        public void SpawnCardOnRandomPosition(CardConfig config)
        {
            if (!gridSpawner.HasEmptyGridPositions())
            {
                Debug.LogError("No empty grid positions!");
                return;
            }
        
            var gridSize = gridSpawner.GridSize;

            var maxX = gridSize.x;
            var maxY = gridSize.y;
            
            Vector2Int randomGridPosition;
            
            do
            {
                var randomX = Random.Range(0, maxX);
                var randomY = Random.Range(0, maxY);
                
                randomGridPosition = new Vector2Int(randomX, randomY);
            } while (gridSpawner.HasCardAtGridPosition(randomGridPosition));
            
            Card.Card newCard=  gridSpawner.SpawnObjectAtGridPosition(cardPrefab.gameObject, randomGridPosition).GetComponent<Card.Card>();
            newCard.Initialize(config);
        }
    }
}