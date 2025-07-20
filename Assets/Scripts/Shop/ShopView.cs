using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;

    [Space(15)]
    [SerializeField] private Button _selectedButton;
    [SerializeField] private Image _buttonBody;
    [SerializeField] private TextMeshProUGUI _buttonText;

    [Space(5)]
    [SerializeField] private Sprite _buySprite;
    [SerializeField] private Sprite _ownedSprite;

    [Space(5)]
    [SerializeField] private Image _resourceIconImage;
    [SerializeField] private Sprite _diamondSprite;
    [SerializeField] private Sprite _coinSprite;

    public void SetSkinName(string _skinName)
    {
        _nameText.text = _skinName;
    }

    // Подумать, что можно сделать с текстом
    public void ButtonUpdate(bool _canSelected, bool _canBuy, int _price, ResourceType _resourceType)
    {
        if (_resourceType == ResourceType.Coin)
        {
            _resourceIconImage.sprite = _coinSprite;
        }

        if (_resourceType == ResourceType.Diamond)
        {
            _resourceIconImage.sprite = _diamondSprite;
        }

        if (!_canSelected)
        {
            _selectedButton.interactable = true;
            _buttonText.text = "ВЫБРАТЬ";
            BuyCheck(_canBuy, _price);
            return;
        }

        _selectedButton.interactable = false;
        _buttonText.text = "ВЫБРАНО";

        BuyCheck(_canBuy, _price);
    }

    private void BuyCheck(bool _canBuy, int _price)
    {
        if (_canBuy)
        {
            Debug.Log("True");
            _buttonBody.sprite = _ownedSprite;
            _resourceIconImage.gameObject.SetActive(false);
            return;
        }

        _buttonBody.sprite = _buySprite;
        _resourceIconImage.gameObject.SetActive(true);

        _buttonText.text = ResourceFormatSingleton.Format(_price).ToString();
    }
}