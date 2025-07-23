using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class SceneTransistion : MonoBehaviour
{
    private CursorController _cursorService;
    private Pause _pauseService;

    [SerializeField] private Blackscreen _blackScreen;

    private void Start()
    {
        _cursorService = GetComponent<CursorController>();
        _pauseService = GetComponent<Pause>();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SceneLoad(int _sceneID)
    {
        _pauseService.OffPauseGame();
        _cursorService.UpdateCursorState(CursorState.UnLocked);
        SceneManager.LoadScene(_sceneID);
    }

    public void SceneLoadFade(int _sceneID)
    {
        _pauseService.OffPauseGame();
        _cursorService.UpdateCursorState(CursorState.UnLocked);

        AsyncLoad(_sceneID).Forget();
    }

    private async UniTaskVoid AsyncLoad(int _id)
    {
        _blackScreen.OpenBlackScreen();

        await UniTask.WaitForSeconds(_blackScreen.Duration);

        SceneManager.LoadScene(_id);
    }
}