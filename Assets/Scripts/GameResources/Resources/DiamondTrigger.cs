using UnityEngine;
using UnityEngine.Events;

public class DiamondTrigger : MonoBehaviour
{
    private GameplayResourceManager _gameplayResourceManager;

    [SerializeField]
    private UnityEvent _dieEvent;

    private void Start()
    {
        _gameplayResourceManager = FindObjectOfType<GameplayResourceManager>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.GetComponent<Player>())
        {
            _gameplayResourceManager.AddDiamond();
            _dieEvent?.Invoke();

            Destroy(gameObject);
        }
    }
}