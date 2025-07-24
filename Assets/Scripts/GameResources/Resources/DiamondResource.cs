using UnityEngine;

public class DiamondResource : ResourceBase
{
    public DiamondResource() : base("Diamonds")
    {
        if (!PlayerPrefs.HasKey("Diamonds"))
        {
            Amount = 0;
            Save();
        }
    }
}