using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private InputType _input;

    public InputType InputType => _input;

    private const string InputTypeKey = "InputType";

    private void Awake()
    {
        LoadInputType();
    }

    public void UpdateInputType(InputType _inputType)
    {
        _input = _inputType;

        Debug.Log(_input);
        SaveInputType();
    }

    private void SaveInputType()
    {
        PlayerPrefs.SetInt(InputTypeKey, (int)_input);
        PlayerPrefs.Save();
    }

    private void LoadInputType()
    {
        if (PlayerPrefs.HasKey(InputTypeKey))
        {
            _input = (InputType)PlayerPrefs.GetInt(InputTypeKey); 
        }
    }
}