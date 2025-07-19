using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Blackscreen _blackScreen;
    [SerializeField] private FlyingPopup _logo;

    private void Awake()
    {
        RunEntryAnimationsAsync().Forget();
    }

    private async UniTaskVoid RunEntryAnimationsAsync()
    {
        _blackScreen.CloseBlackScreen();

        await UniTask.WaitForSeconds(0.2f);

        _logo.Show(1);
    }
}