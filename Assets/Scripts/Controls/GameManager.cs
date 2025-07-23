using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UnityEvent _dieEvent;

    [HideInInspector]
    public float Distance;

    private void Awake()
    {
        Instance = this;
    }

    public void Die()
    {
        _dieEvent?.Invoke();
    }
}