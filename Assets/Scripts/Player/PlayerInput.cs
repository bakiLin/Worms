using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerInput : MonoBehaviour
{
    [Inject]
    private PlayerShoot shoot;

    [Inject]
    private PlayerRotation rotation;

    [Inject]
    private WeaponRotation weaponRotation;

    [Inject]
    private PlayerAnimation playerAnimation;

    private KeyboardInputAction keyboardInputAction;

    private InputAction movementAction, shootAction;

    private void Awake()
    {
        keyboardInputAction = new KeyboardInputAction();
        movementAction = keyboardInputAction.Keyboard.Movement;
        shootAction = keyboardInputAction.Keyboard.Shoot;
    }

    private void OnEnable()
    {
        keyboardInputAction.Enable();

        shootAction.started += (InputAction.CallbackContext context) => shoot.Shoot();

        movementAction.started += (InputAction.CallbackContext context) =>
        {
            rotation.Rotate();
            weaponRotation.Rotate();
            playerAnimation.Walk();
        };

        movementAction.canceled += (InputAction.CallbackContext context) =>
        {
            rotation.StopRotate();
            weaponRotation.StopRotate();
            playerAnimation.StopWalk();
        };
    }

    public Vector2 GetKeyboardInput()
    {
        return movementAction.ReadValue<Vector2>();
    }
}
