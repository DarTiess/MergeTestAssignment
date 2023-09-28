using System;
using Card;

public interface ICoinsUpdate
{
    event Action<int, CardType> UpdateCoinsAmount;
    int GetCoinsAmount();
    void SpendCoins(int value);
}