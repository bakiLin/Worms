using UnityEngine;
using UniRx;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float rayLength, moveSpeed;

    private Rigidbody2D rb;

    private RaycastHit2D hit;

    private CapsuleCollider2D coll;

    private bool onGround, onSlope;

    private Vector2 playerPerpendicular;

    private CompositeDisposable disposable = new CompositeDisposable();

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        SlopeCheck();
        Movement(playerInput.movementAction.ReadValue<Vector2>());
    }

    private void SlopeCheck()
    {
        Vector2 bottom = transform.position - new Vector3(0f, coll.bounds.size.y / 2f);

        if (hit = Physics2D.Raycast(bottom, Vector2.down * rayLength, rayLength, groundLayer))
        {
            playerPerpendicular = Vector2.Perpendicular(hit.normal).normalized;

            onGround = true;
            float angle = Vector2.Angle(hit.normal, Vector2.up);
            onSlope = angle > 0 ? true : false;
        }
        else
        {
            onGround = false;
            onSlope = false;
        }

        Debug.DrawRay(bottom, Vector2.down * rayLength, Color.red);
    }

    public void Movement(Vector2 direction)
    {
        if (onGround && !onSlope)
        {
            rb.velocity = new Vector2(direction.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (onGround && onSlope)
        {
            rb.velocity = new Vector2(-direction.x * playerPerpendicular.x * moveSpeed * Time.fixedDeltaTime, rb.velocity.y);
        }
        else if (!onGround)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
    }

    public void MovementCancel()
    {
        disposable.Clear();
    }
}
