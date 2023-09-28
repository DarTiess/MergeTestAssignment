using System;
using Card;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridBuilder : MonoBehaviour, ICardUpgrade, ICardCollected
{
    [SerializeField] 
    private Transform gridOrigin;
   
    private Card.Card[,] _gameObjectGrid;
    private Vector2Int _gridSize = new Vector2Int(10, 10);
    private float _cellSize = 1f;
    private float _cellSpacing = 0.1f;

    private Card.Card _cardPrefab;
    private CardConfig[] _configs;
    private ICoinsAnimationView _coinsAnimation;
    public Vector2Int GridSize => _gridSize;
    public event Action CardUpgrade;
    public event Action<int, CardType> CollectedCard;
    /// <summary>
    /// Construct
    /// </summary>
    /// <param name="gridSize"></param>
    /// <param name="cellSize"></param>
    /// <param name="cellSpacing"></param>
    /// <param name="cardPrefab"></param>
    /// <param name="configs"></param>
    /// <param name="effect"></param>
    public void Initialize(Vector2Int gridSize, float cellSize, float cellSpacing, 
                           Card.Card cardPrefab, CardConfig[] configs, ICoinsAnimationView coinsAnimation)
    {
        _gridSize = gridSize;
        _cellSize = cellSize;
        _cellSpacing = cellSpacing;
        _gameObjectGrid = new Card.Card[gridSize.x,gridSize.y];
        _cardPrefab = cardPrefab;
        _configs = configs;
        _coinsAnimation = coinsAnimation;
    }
    /// <summary>
    /// Clear
    /// </summary>
    private void OnDisable()
    {
        foreach (Card.Card card in _gameObjectGrid)
        {
            card.CardUpgrade -= OnCardUpgrade;
            card.CollectCard -= OnCollectedCard;
        }
    }
    /// <summary>
    /// Converts a grid position to a world position
    /// </summary>
    /// <param name="gridPosition">Position on the grid</param>
    /// <returns>position in the world</returns>
    private Vector3 GridToWorldPosition(Vector2Int gridPosition)
    {
        var offset = gridOrigin.position;
        return (new Vector3(gridPosition.x * (_cellSize + _cellSpacing), gridPosition.y * (_cellSize + _cellSpacing)) + offset);
    }
    /// <summary>
    /// Sets the object at the given grid position
    /// </summary>
    /// <param name="gameObject">Object to set</param>
    /// <param name="gridPosition">Position on the grid</param>
    private void SetObjectAtGridPosition(Card.Card gameObject, Vector2Int gridPosition)
    {
        _gameObjectGrid[gridPosition.x, gridPosition.y] = gameObject;
        gameObject.transform.position = GridToWorldPosition(gridPosition);
    }

    /// <summary>
    /// Spawns the given gameObject at the given grid position
    /// </summary>
    /// <param name="gameObjectPrefab">Prefab to spawn</param>
    /// <param name="gridPosition">Position on the grid</param>
    /// <returns>Spawned gameObject</returns>
    private Card.Card SpawnObjectAtGridPosition(Card.Card gameObjectPrefab, Vector2Int gridPosition)
    {
        var instance =Instantiate(gameObjectPrefab, gridOrigin);
        instance.CardUpgrade += OnCardUpgrade;
        instance.CollectCard += OnCollectedCard;
        SetObjectAtGridPosition(instance, gridPosition);
        instance.transform.position = GridToWorldPosition(gridPosition);
        return instance;
    }

    /// <summary>
    /// If cards was collected
    /// </summary>
    /// <param name="price">cards price</param>
    /// <param name="type">cards type</param>
    private void OnCollectedCard(int price, CardType type)
    {
        CollectedCard?.Invoke(price, type);
    }

    /// <summary>
    /// Send event if card upgraded
    /// </summary>
    private void OnCardUpgrade()
    {
        CardUpgrade?.Invoke();
    }

    /// <summary>
    /// Create and fill grid
    /// </summary>
    public void FillGrid()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int y = 0; y < _gridSize.y; y++)
            {
                Card.Card newcard = SpawnObjectAtGridPosition(_cardPrefab, new Vector2Int(x, y));
                int rndConfig = Random.Range(0, _configs.Length);
                newcard.Initialize(_configs[rndConfig], _coinsAnimation);
            }
        }
    }

    /// <summary>
    /// Activate object at given position
    /// </summary>
    /// <param name="gridPosition">position on grid</param>
    /// <param name="config">card's config</param>
    public void SpawnObjectAtFreePosition( Vector2Int gridPosition, CardConfig config)
    {
        var instance = _gameObjectGrid[gridPosition.x, gridPosition.y];
        instance.gameObject.SetActive(true);
        SetObjectAtGridPosition(instance, gridPosition);
        instance.transform.position = GridToWorldPosition(gridPosition);
        instance.Initialize(config, _coinsAnimation);
    }

    /// <summary>
    /// Checks if there is active object at the given grid position
    /// </summary>
    /// <param name="gridPosition">Position on the grid</param>
    /// <returns>true if there is an object at the given grid position</returns>
    public bool HasCardAtGridPosition(Vector2Int gridPosition)
    {
        return _gameObjectGrid[gridPosition.x, gridPosition.y].gameObject.activeInHierarchy;
    }
    // <summary>
    /// Checks if there is inactive object on the grid
    /// </summary>
    /// <returns>true if there is an empty grid position available on the grid</returns>
    public bool HasEmptyGridPositions()
    {
        foreach (var gameObject in _gameObjectGrid)
        {
            if (!gameObject.gameObject.activeInHierarchy)
            {
                return true;
            }
        }

        return false;
    }
}