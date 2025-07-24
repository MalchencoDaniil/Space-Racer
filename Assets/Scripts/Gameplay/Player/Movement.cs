using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Player _player;
    private Rigidbody _rigidbody;

    private float _horizontalMovement, _currentSpeed, _horizontalSpeed;
    private Quaternion _targetRotation;

    private Vector3 _bodyStartRotation;

    [Header("References")]
    private PlayerInput _inputHandler;

    [Header("Movement")]
    [SerializeField] private Vector3 _forwardMovementDirection = Vector3.forward;
    [SerializeField] private float _initialSpeed = 10f;
    [SerializeField] private float _maxSpeed = 30f;
    [SerializeField] private float _accelerationRate = 0.5f;
    [SerializeField] private float _distancePerAcceleration = 100f;

    [Space(15)]
    [SerializeField] private float _minHorizontalSpeed = 20;
    [SerializeField] private float _maxHorizontalSpeed = 35;
    [SerializeField] private float _horizontalDampening = 0.5f;

    [Header("Tilt")]
    [SerializeField] private Transform _body;
    [SerializeField] private float _tiltAmount = 10f;
    [SerializeField] private float _tiltSpeed = 5f;

    [Header("Limitations")]
    [SerializeField] private float _maxVelocity = 50f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();

        _inputHandler = GetComponent<PlayerInput>();

        _bodyStartRotation = _body.eulerAngles;

        _currentSpeed = _initialSpeed;
        _horizontalSpeed = _minHorizontalSpeed;


        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
    private void Update()
    {
        _horizontalMovement = _inputHandler.MovementInput().x;
        _targetRotation = Quaternion.Euler(_bodyStartRotation.x, _bodyStartRotation.y, -_horizontalMovement * _tiltAmount);
    }

    private void FixedUpdate()
    {
        SpeedControl();
        ForwardMovement();
        HorizontalMovement();
        Tilt();

        LimitVelocity();
    }

    private void SpeedControl()
    {
        float _distanceTraveled = transform.position.z - _player._distanceSinceLastIncrease;

        if (_distanceTraveled >= _distancePerAcceleration)
        {
            _player._distanceSinceLastIncrease = transform.position.z;
            float increment = _accelerationRate * Time.fixedDeltaTime;

            _currentSpeed = Mathf.Clamp(_currentSpeed + increment, _initialSpeed, _maxSpeed);
            _horizontalSpeed = Mathf.Clamp(_horizontalSpeed + (increment / 2), _minHorizontalSpeed, _maxHorizontalSpeed);
        }
    }

    private void ForwardMovement()
    {
        Vector3 forwardMovement = _forwardMovementDirection.normalized * _currentSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + forwardMovement);
    }

    private void HorizontalMovement()
    {
        float horizontalMovementAmount = _horizontalMovement * _horizontalSpeed * Time.fixedDeltaTime;
        Vector3 horizontalMovement = transform.right * horizontalMovementAmount;
        _rigidbody.MovePosition(_rigidbody.position + horizontalMovement);
    }

    private void Tilt()
    {
        _body.rotation = Quaternion.Slerp(_body.rotation, _targetRotation, _tiltSpeed * Time.fixedDeltaTime);
    }

    private void LimitVelocity()
    {
        if (_rigidbody.velocity.magnitude > _maxVelocity)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _maxVelocity;
        }
    }
}