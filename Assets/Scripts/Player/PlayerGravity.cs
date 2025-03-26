using UnityEngine;
using Zenject;

public class PlayerGravity : MonoBehaviour
{
    [Inject]
    private PlayerGroundContact playerGroundContact;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float rayLength, maxSlopeAngle, gravityScale;

    public bool onSlope {  get; private set; }

    public Vector2 perpendicular {  get; private set; }

    private Vector2 bottom, gravity;

    private float groundAngle;

    private Collider2D coll;

    private RaycastHit2D hit;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    public void CheckSlope()
    {
        bottom = coll.bounds.center - new Vector3(0f, coll.bounds.size.y / 2);

        if (hit = Physics2D.Raycast(bottom, Vector2.down, rayLength, groundLayer))
        {
            groundAngle = Vector2.Angle(hit.normal, Vector2.up);
            onSlope = groundAngle > 0f ? true : false;
            perpendicular = Vector2.Perpendicular(hit.normal).normalized;
        }
    }

    public void Gravity(Rigidbody2D rb)
    {
        if (groundAngle > maxSlopeAngle || groundAngle == 0f)
        {
            rb.AddForce(Physics.gravity * 1f, ForceMode2D.Force);
        }
        else if (groundAngle > 0f)
        {
            gravity = (playerGroundContact.contactPoint - transform.position) * Physics2D.gravity.magnitude;
            rb.AddForce(gravity * gravityScale, ForceMode2D.Force);
        }
    }

    public bool IsTouchingGround() => hit.collider != null;
}
