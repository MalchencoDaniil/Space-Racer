using UnityEngine;

[CreateAssetMenu(fileName = "CharacterItem", menuName = "Shop/CharacterItem")]
public class CharacterSkinItem : ShopItem
{
    [field: SerializeField] public Movement Player { get; private set; }
    [field: SerializeField] public CharacterSkins SkinType { get; private set; }
    [SerializeField] public bool _canBuy;

    private void OnEnable()
    {
        if (SkinType == CharacterSkins.Wing) // Start Plane
            return;

        _canBuy = PlayerPrefs.GetInt($"SkinBought_{SkinType.ToString()}", 0) == 1;
    }
}