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

        Vector2 _startPosition = _targetBody.anchoredPosition;

        Vector2 _firstPosition = new Vector2(_startPosition.x, -Screen.height / 3);
        Vector2 _secondPosition = new Vector2(_startPosition.x, -Screen.height / 4);

        _animation
            .Append(_body.DOFade(1, _fadeDuration))
            .Join(_body.rectTransform.DOAnchorPos(_firstPosition, _moveSpeed).From(_startPosition))
            .Append(_body.rectTransform.DOAnchorPos(_secondPosition, _moveSpeed * 2).From(_firstPosition));
    }
}