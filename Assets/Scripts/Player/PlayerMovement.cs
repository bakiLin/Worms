using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject]
    private PlayerInput input;

    [Inject]
    private PlayerGravity gravity;

    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    private Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Movement(input.GetKeyboardInput());
    }

    private void Movement(Vector2 moveInput)
    {
        if (gravity.IsMovable())
        {
            velocity = gravity.GetPerpendicular() * speed * moveInput.x;
            rb.velocity = velocity;
        }
    }
}
