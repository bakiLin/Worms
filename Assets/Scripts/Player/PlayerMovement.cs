using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject]
    private PlayerInput playerInput;

    [Inject]
    private PlayerGravity playerGravity;

    [Inject]
    private PlayerGroundContact playerGroundContact;

    [SerializeField]
    private float speed;

    private Rigidbody2D rb;

    private float directionalSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerGravity.CheckSlope();
        Movement(playerInput.GetKeyboardInput());
        playerGravity.Gravity(rb);
    }

    private void Movement(Vector2 direction)
    {
        if (playerGravity.IsTouchingGround())
        {
            directionalSpeed = speed * Time.fixedDeltaTime * direction.x;
            if (!playerGravity.onSlope) rb.velocity = new Vector2(directionalSpeed, rb.velocity.y);
            else rb.velocity = new Vector2(-playerGravity.perpendicular.x, -playerGravity.perpendicular.y) * directionalSpeed;
        }
    }
}
