using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;

    [Space(15)]
    [SerializeField] private Button _selectedButton;
    [SerializeField] private TextMeshProUGUI _buttonText;

    public void SetSkinName(string _skinName)
    {
        _nameText.text = _skinName;
    }

    // Подумать, что можно сделать с текстом
    public void ButtonUpdate(bool _canSelected)
    {
        if (!_canSelected)
        {
            _selectedButton.interactable = true;
            _buttonText.text = "ВЫБРАТЬ";
            return;
        }

        _selectedButton.interactable = false;
        _buttonText.text = "ВЫБРАНО";
    }
}