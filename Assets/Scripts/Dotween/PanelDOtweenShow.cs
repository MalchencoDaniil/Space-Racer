using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

public class PanelDOtweenShow : MonoBehaviour
{
    [SerializeField] private Image _panelImage;
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private bool _canPaused = false;

    private Pause _pauseController;
    private float _initialAlpha;

    private CancellationTokenSource _cts;
    private CancellationTokenSource _closeCts;

    private void Start()
    {
        _pauseController = FindObjectOfType<Pause>();

        if (_panelImage != null)
        {
            _initialAlpha = _panelImage.color.a;
            _panelImage.color = new Color(_panelImage.color.r, _panelImage.color.g, _panelImage.color.b, 0);
        }

        _cts = new CancellationTokenSource();
        _closeCts = new CancellationTokenSource();
    }

    private void OnDestroy()
    {
        _cts.Cancel();
        _cts.Dispose();
        _closeCts.Cancel();
        _closeCts.Dispose();
    }

    public void Show()
    {
        if (_panelImage != null)
        {
            _closeCts.Cancel();
            _closeCts = new CancellationTokenSource();

            ShowLogic(_panelImage, _fadeDuration, _cts.Token).Forget();
        }
    }

    private async UniTaskVoid ShowLogic(Image panelImage, float duration, CancellationToken cancellationToken)
    {
        panelImage.gameObject.SetActive(true);

        Tween tween = panelImage.DOColor(new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, _initialAlpha), duration);

        if (cancellationToken.IsCancellationRequested)
        {
            tween.Kill();
            panelImage.gameObject.SetActive(false);
            return;
        }

        try
        {
            await tween.AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(cancellationToken);
        }
        catch (System.OperationCanceledException)
        {
            tween.Kill();
            panelImage.gameObject.SetActive(false);
            return;
        }

        if (_canPaused)
        {
            _pauseController.OnPauseGame();
        }
    }

    public void Close()
    {
        if (_panelImage != null)
        {
            _cts.Cancel();
            _cts = new CancellationTokenSource();

            CloseLogic(_panelImage, _fadeDuration, _closeCts.Token).Forget();
        }
    }

    private async UniTaskVoid CloseLogic(Image panelImage, float duration, CancellationToken cancellationToken)
    {
        Tween tween = panelImage.DOColor(new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, 0), duration);

        if (cancellationToken.IsCancellationRequested)
        {
            tween.Kill();
            Debug.Log("Close Animation cancelled before start.");
            panelImage.gameObject.SetActive(false); // Ensure it's closed
            return;
        }

        try
        {
            await tween.AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(cancellationToken);
        }
        catch (System.OperationCanceledException)
        {
            tween.Kill();
            panelImage.gameObject.SetActive(false);
            return;
        }

        panelImage.gameObject.SetActive(false);
    }
}