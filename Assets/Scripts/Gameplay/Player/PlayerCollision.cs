using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private LayerMask _collisionMask;

    private void OnTriggerEnter(Collider _other)
    {
        if (((1 << _other.gameObject.layer) & _collisionMask) != 0)
        {
            GameManager.Instance.Die();
            gameObject.SetActive(false);
        }
    }
}