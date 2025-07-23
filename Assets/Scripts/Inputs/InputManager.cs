using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [HideInInspector] public InputActions InputActions;

    private void Awake()
    {
        Instance = this;

        InputActions = new InputActions();
    }

    private void OnEnable()
    {
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}