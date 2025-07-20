using UnityEngine;

public class CoinResource : ResourceBase
{
    public CoinResource() : base("Coins")
    {
        if (!PlayerPrefs.HasKey("Coins"))
        {
            Amount = 1000;
            Save();
        }
    }
}