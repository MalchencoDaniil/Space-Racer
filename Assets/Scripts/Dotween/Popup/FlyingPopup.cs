using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FlyingPopup : Popup
{
    private Vector2 _bodyStartPosition;

    [Header("References")]
    [SerializeField] private Image _body;
    [SerializeField] private RectTransform _targetBody;
    [SerializeField] private Color _popupStartColor = Color.white;

    [Header("Animation Stats")]
    [SerializeField] private float _fadeDuration = 1.0f;
    [SerializeField] private float _moveSpeed = 1.0f;

    private void Start()
    {
        _body.color = _popupStartColor;
        _bodyStartPosition = _body.rectTransform.anchoredPosition;
    }

    public override void Show(float _duration)
    {
        Sequence _animation = DOTween.Sequence();

        Vector2 _targetPosition = _targetBody.anchoredPosition;

        // Чуть дальше таргета (например, на 100 пикселей выше)
        Vector2 _overshootPosition = _targetPosition - new Vector2(0, 100f);

        _body.rectTransform.anchoredPosition = _bodyStartPosition;
        _body.color = _popupStartColor;

        _animation.Append(_body.DOFade(1, _fadeDuration)).Join(_body.rectTransform.DOAnchorPos(_overshootPosition, _moveSpeed)).Append(_body.rectTransform.DOAnchorPos(_targetPosition, _moveSpeed * 0.5f));
    }
}