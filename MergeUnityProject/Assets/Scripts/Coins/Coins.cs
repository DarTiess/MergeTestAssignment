using System;
using Card;

public class Coins : ICoinsUpdate, IDisposable
{
    private ICardCollected _cardCollected;
    private CoinData _coinData;
    public event Action<int, CardType> UpdateCoinsAmount;

    /// <summary>
    /// Construct
    /// </summary>
    /// <param name="cardCollected"></param>
    public Coins(ICardCollected cardCollected)
    {
        _cardCollected = cardCollected;
        _cardCollected.CollectedCard += OnChangeCoinsAmount;
        _coinData = new CoinData();
        if (_coinData.Coins <= 0)
        {
            _coinData.Coins = 10;
        }
    }
    /// <summary>
    /// Send coins Amount
    /// </summary>
    /// <returns>coins</returns>
    public int GetCoinsAmount()
    {
        return _coinData.Coins;
    }
    /// <summary>
    /// Spend Coins
    /// </summary>
    /// <param name="value"></param>
    public void SpendCoins(int value)
    { 
        _coinData.Coins -= value;
    }
    /// <summary>
    /// Change coins amount
    /// make Action
    /// </summary>
    /// <param name="price"></param>
    /// <param name="type"></param>
    private void OnChangeCoinsAmount(int price, CardType type)
    {
        _coinData.Coins += price;
        UpdateCoinsAmount?.Invoke(_coinData.Coins, type);
    }
    /// <summary>
    /// On Destroy Clear
    /// </summary>
    public void Dispose()
    {
        _cardCollected.CollectedCard -= OnChangeCoinsAmount;
    }
}