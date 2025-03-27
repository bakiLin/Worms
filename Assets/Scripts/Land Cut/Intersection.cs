using UnityEngine;

public class Intersection
{
    private bool isIntersecting;

    private float denominator, a, b;

    public bool IsIntersecting(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
    {
        isIntersecting = false;
        denominator = (p4.y - p3.y) * (p2.x - p1.x) - (p4.x - p3.x) * (p2.y - p1.y);
        
        if (denominator != 0)
        {
            a = ((p4.x - p3.x) * (p1.y - p3.y) - (p4.y - p3.y) * (p1.x - p3.x)) / denominator;
            b = ((p2.x - p1.x) * (p1.y - p3.y) - (p2.y - p1.y) * (p1.x - p3.x)) / denominator;        
            if (a >= 0 && a <= 1 && b >= 0 && b <= 1) isIntersecting = true;
        }

        return isIntersecting;
    }

    public Vector2 GetIntersection(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
    {
        float top = (D.x - C.x) * (A.y - C.y) - (D.y - C.y) * (A.x - C.x);
        float bottom = (D.y - C.y) * (B.x - A.x) - (D.x - C.x) * (B.y - A.y);
        return Vector2.Lerp(A, B, top / bottom);
    }
}