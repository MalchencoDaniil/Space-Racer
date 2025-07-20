using Cysharp.Threading.Tasks;
using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    private Shop _shop;

    [SerializeField] private Blackscreen _blackScreen;
    [SerializeField] private FlyingPopup _logo;

    private void Awake()
    {
        ShopInitialize();

        RunEntryAnimationsAsync().Forget();
    }

    private void ShopInitialize()
    {
        _shop = FindObjectOfType<Shop>();
        _shop.Initialize();
    }

    private async UniTaskVoid RunEntryAnimationsAsync()
    {
        _blackScreen.CloseBlackScreen();

        await UniTask.WaitForSeconds(0.2f);

        _logo.Show(1);
    }
}