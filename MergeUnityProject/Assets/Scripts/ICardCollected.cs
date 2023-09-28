using System;
using Card;

public interface ICardCollected
{
    event Action<int, CardType> CollectedCard;
}