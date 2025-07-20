using TMPro;
using UnityEngine;
using Zenject;

public class ResourceView : MonoBehaviour
{
    private IResourceService _resourceService;

    IResource _coin, _diamond;

    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _diamondText;

    [Inject]
    public void Construct(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    private void Start()
    {
        _coin = _resourceService.GetResource(ResourceType.Coin);
        _diamond = _resourceService.GetResource(ResourceType.Diamond);

        _coin.OnChanged += UpdateText;
        _diamond.OnChanged += UpdateText;

        UpdateText();
    }

    private string Format(int _amount)
    {
        return _amount >= 1000 ? (_amount / 1000) + "k" : _amount.ToString();
    }

    public void UpdateText()
    {
        _coinText.text = Format(_resourceService.GetResource(ResourceType.Coin).Amount);
        _diamondText.text = Format(_resourceService.GetResource(ResourceType.Diamond).Amount);
    }

    private void OnDestroy()
    {
        _coin.OnChanged -= UpdateText;
        _diamond.OnChanged -= UpdateText;
    }
}