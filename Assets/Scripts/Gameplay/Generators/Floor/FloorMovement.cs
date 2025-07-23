using UnityEngine;

public class FloorMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5;
    [SerializeField] private Vector3 _movementDirection = Vector3.forward;

    private void Update()
    {
        transform.Translate(_movementDirection * _movementSpeed * 10 * Time.deltaTime);
    }
}