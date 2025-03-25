using UnityEngine;

public class ColliderRenderer : MonoBehaviour
{
    private PolygonCollider2D coll;

    private MeshFilter meshFilter;

    private void Awake()
    {
        coll = GetComponent<PolygonCollider2D>();
        meshFilter = transform.Find("Mesh").GetComponent<MeshFilter>();
    }

    private void Start()
    {
        CreateMesh();
    }

    public void CreateMesh()
    {
        meshFilter.mesh = coll.CreateMesh(true, true);
    }
}
