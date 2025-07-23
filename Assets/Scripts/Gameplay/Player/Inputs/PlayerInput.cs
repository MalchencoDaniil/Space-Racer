using UnityEngine;

public class PlayerInput
{
    public Vector2 MovementInput()
    {
        return InputManager.Instance.InputActions.Player.Movement.ReadValue<Vector2>();
    }
}