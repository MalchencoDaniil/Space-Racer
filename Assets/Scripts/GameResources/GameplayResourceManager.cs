using TMPro;
using UnityEngine;
using Zenject;

public class GameplayResourceManager : MonoBehaviour
{
    private int _diamond;
    private int _coin;

    private IResourceService _resourceService;

    IResource _coinResource, _diamondResource;

    [Inject]
    public void Construct(IResourceService resourceService)
    {
        _resourceService = resourceService;
        // ResourceFormatSingleton
    }

    [SerializeField]
    private TextMeshProUGUI _coinText, _diamondText;

    [SerializeField] private float coinDistanceStep = 50f;
    [SerializeField] private float nextCoinDistance = 50f;
    [SerializeField] private int _coinAmount = 50;

    private void Start()
    {
        _coinResource = _resourceService.GetResource(ResourceType.Coin);
        _diamondResource = _resourceService.GetResource(ResourceType.Diamond);
    }

    private void Update()
    {
        float distance = GameManager.Instance.Distance;

        if (distance >= nextCoinDistance)
        {
            _coin += _coinAmount;
            nextCoinDistance += coinDistanceStep;
        }
    }

    public void Result()
    {
        Debug.Log(_resourceService);

        _coinResource.Add(_coin);
        _diamondResource.Add(_diamond);

        _coinText.text = ResourceFormatSingleton.Format(_coin);
        _diamondText.text = ResourceFormatSingleton.Format(_diamond);
    }

    public void AddDiamond()
    {
        _diamond++;
    }
}