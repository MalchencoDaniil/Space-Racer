using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    private PlayerView _playerView;
    public PlayerView PlayerView => _playerView;

    [HideInInspector]
    public float _distanceSinceLastIncrease = 0f;

    private void Start()
    {
        _playerView = FindObjectOfType<PlayerView>();
    }

    private void Update()
    {
        DistanceControl();
    }

    private void DistanceControl()
    {
        _playerView.UpdateDistanceText(_distanceSinceLastIncrease);
        GameManager.Instance.Distance = _distanceSinceLastIncrease;
    }
}