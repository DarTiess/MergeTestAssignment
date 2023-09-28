using UnityEngine;

public class CoinData
{
    private const string MONEY = "Coins";
    public int Coins
    {
        get { return PlayerPrefs.GetInt(MONEY); }
        set { PlayerPrefs.SetInt(MONEY, value); }
    }
}