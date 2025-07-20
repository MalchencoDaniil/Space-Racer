using UnityEngine;

public class ResourceFormatSingleton : MonoBehaviour
{
    public static ResourceFormatSingleton Instance;

    private void Awake()
    {
        Instance = this;
    }

    public static string Format(int _amount)
    {
        return _amount >= 1000 ? (_amount / 1000) + "k" : _amount.ToString();
    }
}