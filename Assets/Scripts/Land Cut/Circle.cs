using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] 
    private int sides;

    private PolygonCollider2D coll;

    private void Awake()
    {
        coll = GetComponent<PolygonCollider2D>();
    }

    private void Start()
    {
        coll.CreatePrimitive(sides, Vector2.one);
    }
}
