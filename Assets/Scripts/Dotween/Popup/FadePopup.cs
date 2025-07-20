using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FadePopup : Popup
{
    [SerializeField] private Image _body;

    public override void Show(float _duration)
    {
        _body.DOFade(1, _duration);
    }

    public override void Hide(float _duration)
    {
        _body.DOFade(0, _duration);
    }
}