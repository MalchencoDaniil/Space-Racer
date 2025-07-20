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
        // ResourceFormatSingleton
    }

    private void Start()
    {
        _coin = _resourceService.GetResource(ResourceType.Coin);
        _diamond = _resourceService.GetResource(ResourceType.Diamond);

        _coin.OnChanged += UpdateText;
        _diamond.OnChanged += UpdateText;

        UpdateText();
    }

    public void UpdateText()
    {
        _coinText.text = ResourceFormatSingleton.Format(_resourceService.GetResource(ResourceType.Coin).Amount);
        _diamondText.text = ResourceFormatSingleton.Format(_resourceService.GetResource(ResourceType.Diamond).Amount);
    }

    private void OnDestroy()
    {
        _coin.OnChanged -= UpdateText;
        _diamond.OnChanged -= UpdateText;
    }
}