using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private ShopContent _shopContent;
    [SerializeField] private Transform _skinSpawnPoint;

    private ShopModel _shopModel;
    private ShopView _shopView;

    private void Awake()
    {
        _shopView = FindObjectOfType<ShopView>();
    }

    public void Initialize()
    {
        _shopModel = new ShopModel();

        _shopModel.SelectedCharacterID = PlayerPrefs.GetInt(ShopPrefsNames.SelectedCharacterID, 0);

        if (_shopModel.SelectedCharacterID > _shopContent.CharacterSkinItems.Count() - 1)
        {
            _shopModel.SelectedCharacterID = 0;
            PlayerPrefs.SetInt(ShopPrefsNames.SelectedCharacterID, 0);
        }

        _shopModel.CurrentCharacterID = _shopModel.SelectedCharacterID;

        int id = 0;
        foreach (var item in _shopContent.CharacterSkinItems)
        {
            Skin newSkin = Instantiate(item.Model, _skinSpawnPoint.position, Quaternion.identity);
            newSkin.transform.SetParent(transform);

            newSkin.gameObject.SetActive(false);
            _shopModel.AllSkins.Add(newSkin);

            if (id == _shopModel.SelectedCharacterID)
                newSkin.gameObject.SetActive(true);

            id++;
        }

        UpdateView();
    }

    public void Toggle()
    {
        _shopModel.CurrentCharacterID = _shopModel.SelectedCharacterID;

        _shopModel.AllSkins.ForEach(_skin => _skin.gameObject.SetActive(false));
        _shopModel.AllSkins[_shopModel.SelectedCharacterID].gameObject.SetActive(true);

        UpdateView();
    }

    public void LeftMove()
    {
        _shopModel.AllSkins[_shopModel.CurrentCharacterID].gameObject.SetActive(false);

        _shopModel.CurrentCharacterID--;
        if (_shopModel.CurrentCharacterID < 0)
            _shopModel.CurrentCharacterID = _shopModel.AllSkins.Count - 1;

        _shopModel.AllSkins[_shopModel.CurrentCharacterID].gameObject.SetActive(true);

        UpdateView();
    }

    public void RightMove()
    {
        _shopModel.AllSkins[_shopModel.CurrentCharacterID].gameObject.SetActive(false);

        _shopModel.CurrentCharacterID++;
        if (_shopModel.CurrentCharacterID >= _shopModel.AllSkins.Count)
            _shopModel.CurrentCharacterID = 0;

        _shopModel.AllSkins[_shopModel.CurrentCharacterID].gameObject.SetActive(true);

        UpdateView();
    }

    public void Select()
    {
        PlayerPrefs.SetInt(ShopPrefsNames.SelectedCharacterID, _shopModel.CurrentCharacterID);
        _shopModel.SelectedCharacterID = _shopModel.CurrentCharacterID;

        UpdateView();
    }

    private void UpdateView()
    {
        var _skin = _shopContent.CharacterSkinItems.ElementAt(_shopModel.CurrentCharacterID);

        var _name = _skin.SkinType.ToString();
        _shopView.SetSkinName(_name);
        _shopView.ButtonUpdate(_shopModel.CurrentCharacterID == _shopModel.SelectedCharacterID);
    }
}