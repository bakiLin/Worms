using UnityEngine;

public class PlayerGroundContact : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;

    private ContactPoint2D contactPoint;

    private int contacts;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(groundLayer, 2)) contacts++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(groundLayer, 2)) contacts--;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(groundLayer, 2)) contactPoint = collision.contacts[0];
    }

    public bool IsTouchingGround() => contacts > 0;

    public float ContactAngle() => Vector2.Angle(contactPoint.normal, Vector2.up);

    public Vector3 GetContactPoint() => contactPoint.point;

    public Vector3 GetNormal() => contactPoint.normal;
}
