using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputHandler _inputHandler;

    [Header("Screen Zones")]
    [SerializeField]
    private float leftZoneThreshold = 0.3f;

    [SerializeField]
    private float rightZoneThreshold = 0.7f;

    private void Start()
    {
        _inputHandler = FindObjectOfType<InputHandler>();
    }

    public Vector2 MovementInput()
    {
        if (_inputHandler.InputType == InputType.Accelerometer)
            return Input.acceleration;
        else
        {
            float _horizontalInput = 0f;

            if (Input.touchCount > 0)
            {
                Touch _touch = Input.GetTouch(0);

                float _touchPositionX = _touch.position.x / Screen.width;

                if (_touchPositionX < leftZoneThreshold)
                {
                    _horizontalInput = -1f;
                }
                else if (_touchPositionX > rightZoneThreshold)
                {
                    _horizontalInput = 1f;
                }
            }

            return new Vector2(_horizontalInput, 0);
        }
    }
}