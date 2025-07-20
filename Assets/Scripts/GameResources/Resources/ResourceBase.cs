using System;
using UnityEngine;

public abstract class ResourceBase : IResource
{
    public int Amount { get; protected set; }
    protected string SaveKey;

    protected ResourceBase(string saveKey)
    {
        SaveKey = saveKey;
        Load();
    }

    public event Action OnChanged;

    public virtual void Add(int value)
    {
        Amount += value;
        Save();
        OnChanged?.Invoke();
    }
    public virtual bool TrySpend(int value)
    {
        if (Amount < value) return false;
        Amount -= value;
        Save();
        OnChanged?.Invoke(); 
        return true;
    }

    public virtual void Save()
    {
        PlayerPrefs.SetInt(SaveKey, Amount);
        PlayerPrefs.Save();
    }

    public virtual void Load()
    {
        Amount = PlayerPrefs.GetInt(SaveKey, 0);
    }
}