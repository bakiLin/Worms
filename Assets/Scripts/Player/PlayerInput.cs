using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement playerMovement;

    private KeyboardInputAction keyboardInputAction;

    public InputAction movementAction;

    private void Awake()
    {
        keyboardInputAction = new KeyboardInputAction();
        movementAction = keyboardInputAction.Keyboard.Movement;
    }

    private void OnEnable()
    {
        keyboardInputAction.Enable();
    }
}
