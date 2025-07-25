using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [field: SerializeField] public Skin Model {  get; private set; }
    [SerializeField, Range(0, 999999)] public int Price;
    [SerializeField] public ResourceType ResourceType;
}