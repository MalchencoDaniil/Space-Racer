using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UnityEvent _dieEvent;

    [HideInInspector]
    public Player _player;

    [HideInInspector]
    public float Distance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    public void Die()
    {
        _dieEvent?.Invoke();
    }

    public void DisablePlayer()
    {
        _player.gameObject.SetActive(false);
    }
}