using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private ParticleSystem _dieEffect;
    [SerializeField] private AudioClip _dieSound;

    private void OnTriggerEnter(Collider _other)
    {
        if (((1 << _other.gameObject.layer) & _collisionMask) != 0)
        {
            PlayerDie().Forget();
        }
    }

    private async UniTaskVoid PlayerDie()
    {
        AllAudioSource.Instance.SoundAudioSource.PlayOneShot(_dieSound);
        gameObject.SetActive(false);

        if ((int)GameManager.Instance.Distance > PlayerPrefs.GetInt("BestDistance"))
            PlayerPrefs.SetInt("BestDistance", (int)GameManager.Instance.Distance);

        ParticleSystem _explosion = Instantiate(_dieEffect, transform.position, Quaternion.identity);
        _explosion.Play();

        await UniTask.WaitForSeconds(2);

        GameManager.Instance.Die();
    }
}