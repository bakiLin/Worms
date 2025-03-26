using UnityEngine;

[ExecuteAlways]
public class ColliderRenderer : MonoBehaviour
{
    [SerializeField]
    private PolygonCollider2D coll;

    [SerializeField]
    private MeshFilter meshFilter;

    //private void Awake()
    //{
    //    coll = GetComponent<PolygonCollider2D>();
    //    meshFilter = transform.Find("Mesh").GetComponent<MeshFilter>();
    //}

    private void Start()
    {
        CreateMesh();
    }

    public void CreateMesh()
    {
        meshFilter.mesh = coll.CreateMesh(true, true);
    }

    private void OnValidate()
    {
        CreateMesh();
    }
}
