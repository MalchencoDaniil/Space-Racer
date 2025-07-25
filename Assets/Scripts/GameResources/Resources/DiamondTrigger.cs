using UnityEngine;
using UnityEngine.Events;

public class DiamondTrigger : MonoBehaviour
{
    private GameplayResourceManager _gameplayResourceManager;

    [SerializeField] private AudioClip _pickSound;

    private void Start()
    {
        _gameplayResourceManager = FindObjectOfType<GameplayResourceManager>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.GetComponent<Player>())
        {
            _gameplayResourceManager.AddDiamond();
            AllAudioSource.Instance.SoundAudioSource.PlayOneShot(_pickSound);

            Destroy(gameObject);
        }
    }
}