using UnityEngine;

public class PlayerGroundContact : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;

    public Vector3 contactPoint { get; private set; }

    private int contacts;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(groundLayer, 2)) contacts++;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(groundLayer, 2))
        {
            contactPoint = collision.contacts[0].point;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == Mathf.Log(groundLayer, 2)) contacts--;
    }

    public bool IsTouchingGround() => contacts > 0;
}
