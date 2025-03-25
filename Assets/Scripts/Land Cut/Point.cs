using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Point
{
    public Vector2 position;
    public Point nextPoint;
    public Segment landSegment, circleSegment;
    public bool isCross;
}

[System.Serializable]
public class Line
{
    public List<Point> points;
    public List<Segment> segments;
}

public class Segment
{
    public Point A, B;
    public List<Point> crossPoints = new List<Point>();
}
