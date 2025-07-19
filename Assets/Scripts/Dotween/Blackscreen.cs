using Cysharp.Threading.Tasks;
using UnityEngine;

public class Blackscreen : MonoBehaviour
{
    [SerializeField] private Popup _screenPopup;
    [SerializeField] private float _durationTime = 0.8f;

    public void OpenBlackScreen() => Open(_durationTime).Forget();

    public void CloseBlackScreen() => Close(_durationTime).Forget();

    private async UniTaskVoid Open(float _time)
    {
        _screenPopup.Show(_time);

        await UniTask.WaitForSeconds(_time);
    }

    private async UniTaskVoid Close(float _time)
    {
        _screenPopup.Hide(_time);

        await UniTask.WaitForSeconds(_time);

        _screenPopup.gameObject.SetActive(false);
    }
}