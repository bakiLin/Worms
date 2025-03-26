using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private KeyboardInputAction keyboardInputAction;

    private InputAction movementAction;

    private void Awake()
    {
        keyboardInputAction = new KeyboardInputAction();
        movementAction = keyboardInputAction.Keyboard.Movement;
    }

    private void OnEnable()
    {
        keyboardInputAction.Enable();
    }

    public Vector2 GetKeyboardInput()
    {
        return movementAction.ReadValue<Vector2>();
    }
}
