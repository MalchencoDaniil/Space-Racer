using UnityEngine;
using UnityEngine.UI;

public class InputView : MonoBehaviour
{
    [SerializeField] private Toggle _accelerometerToggle;
    [SerializeField] private Toggle _touchToggle;

    private InputHandler _inputHandler;

    private void Start()
    {
        _inputHandler = GetComponent<InputHandler>();

        _accelerometerToggle.onValueChanged.AddListener(OnAccelerometerToggleChanged);
        _touchToggle.onValueChanged.AddListener(OnTouchToggleChanged);

        InputTypeCheck();
    }

    private void InputTypeCheck()
    {
        switch (_inputHandler.InputType)
        {
            case InputType.Accelerometer:
                _accelerometerToggle.isOn = true;
                _touchToggle.isOn = false;
                _accelerometerToggle.interactable = false;
                _touchToggle.interactable = true;
                break;
            case InputType.Touch:
                _touchToggle.isOn = true;
                _accelerometerToggle.isOn = false;
                _touchToggle.interactable = false;
                _accelerometerToggle.interactable = true;
                break;
        }
    }

    private void OnTouchToggleChanged(bool isOn)
    {
        if (isOn)
        {
            if (_accelerometerToggle.isOn)
            {
                _accelerometerToggle.isOn = false;
                _accelerometerToggle.interactable = true;
            }

            _touchToggle.interactable = false;
            _inputHandler.UpdateInputType(InputType.Touch);
        }
    }

    private void OnAccelerometerToggleChanged(bool isOn)
    {
        if (isOn)
        {
            if (_touchToggle.isOn)
            {
                _touchToggle.isOn = false;
                _touchToggle.interactable = true;
            }

            _accelerometerToggle.interactable = false;
            _inputHandler.UpdateInputType(InputType.Accelerometer);
        }
    }
}