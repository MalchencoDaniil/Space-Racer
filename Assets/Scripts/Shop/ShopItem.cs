using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [field: SerializeField] public Skin Model {  get; private set; }
    [SerializeField, Range(0, 9999)] public float Price;
}