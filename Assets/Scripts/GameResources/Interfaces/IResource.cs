using System;

public interface IResource
{
    int Amount { get; }
    event Action OnChanged;
    void Add(int value);
    bool TrySpend(int value);
    void Save();
    void Load();
}