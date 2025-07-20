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
    [SerializeField] private Transform _coinImage;

    public void SetSkinName(string _skinName)
    {
        _nameText.text = _skinName;
    }

    // Подумать, что можно сделать с текстом
    public void ButtonUpdate(bool _canSelected, bool _canBuy, float _price)
    {
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

    private void BuyCheck(bool _canBuy, float _price)
    {
        if (_canBuy)
        {
            Debug.Log("True");
            _buttonBody.sprite = _ownedSprite;
            _coinImage.gameObject.SetActive(false);
            return;
        }

        _buttonBody.sprite = _buySprite;
        _coinImage.gameObject.SetActive(true);

        _buttonText.text = (_price / 1000).ToString() + "k";
    }
}