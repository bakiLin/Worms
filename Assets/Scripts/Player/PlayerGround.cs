using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerGround : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;

    private BoxCollider2D groundCheckCollider;

    private int overlapCount;

    private void Awake()
    {
        groundCheckCollider = transform.Find("Ground Check").GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        groundCheckCollider.OnTriggerEnter2DAsObservable().Subscribe(collider =>
        {
            if (collider.gameObject.layer == Mathf.Log(groundLayer.value, 2)) overlapCount++;
        });

        groundCheckCollider.OnTriggerExit2DAsObservable().Subscribe(collider =>
        {
            if (collider.gameObject.layer == Mathf.Log(groundLayer.value, 2)) overlapCount--;
        });
    }

    public bool IsGrounded()
    {
        return overlapCount > 0;
    }
}
