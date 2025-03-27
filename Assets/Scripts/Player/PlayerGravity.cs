using UnityEngine;
using Zenject;

public class PlayerGravity : MonoBehaviour
{
    [Inject]
    private PlayerGroundContact groundContact;

    [SerializeField]
    private float maxSlopeAngle;

    private Rigidbody2D rb;

    private Vector3 perpendicular;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate()
    {
        SlopeCheck();
        Gravity();
    }

    private void SlopeCheck()
    {
        if (groundContact.IsTouchingGround())
        {
            perpendicular = Vector2.Perpendicular(groundContact.GetNormal());
        }
    }

    private void Gravity()
    {
        if (!groundContact.IsTouchingGround() || perpendicular.x + perpendicular.y >= 0.99f)
        {
            rb.AddForce(Physics2D.gravity, ForceMode2D.Force);
        } 
        else if (groundContact.ContactAngle() > maxSlopeAngle)
        {
            rb.AddForce(perpendicular, ForceMode2D.Force);
        }
    }

    public Vector2 GetPerpendicular() => -perpendicular;

    public bool IsMovable() => groundContact.IsTouchingGround() && groundContact.ContactAngle() < maxSlopeAngle;
}
