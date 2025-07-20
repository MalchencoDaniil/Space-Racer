using UnityEngine;

[CreateAssetMenu(fileName = "CharacterItem", menuName = "Shop/CharacterItem")]
public class CharacterSkinItem : ShopItem
{
    [field: SerializeField] public CharacterSkins SkinType { get; private set; }
}