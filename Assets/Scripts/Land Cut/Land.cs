using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    private PolygonCollider2D coll;

    private ColliderRenderer colliderRenderer;

    private void Awake()
    {
        coll = GetComponent<PolygonCollider2D>();
        colliderRenderer = GetComponent<ColliderRenderer>();
    }

    public void SetPath(List<List<Point>> paths)
    {
        coll.pathCount = paths.Count;
        for (int i = 0; i < paths.Count; i++)
        {
            List<Vector2> path = new List<Vector2>();
            for (int j = 0; j < paths[i].Count; j++) path.Add(paths[i][j].Position);
            coll.SetPath(i, path);
        }
        colliderRenderer.CreateMesh();
    }
}
