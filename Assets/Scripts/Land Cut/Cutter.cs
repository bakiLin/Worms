using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    [SerializeField] 
    private PolygonCollider2D landCollider;

    [SerializeField] 
    private int testIterations = 10;

    private PolygonCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<PolygonCollider2D>();
    }

    public void DoCut()
    {
        // Делаем из коллайдера круга объект Line
        List<Vector2> circlePointsPositions = circleCollider.GetPath(0).ToList();
        for (int i = 0; i < circlePointsPositions.Count; i++)
            circlePointsPositions[i] = circleCollider.transform.TransformPoint(circlePointsPositions[i]);
        Line circleLine = LineFromCollider(circlePointsPositions);

        List<List<Point>> allSplines = new List<List<Point>>();
        for (int i = 0; i < landCollider.pathCount; i++) // Проходимся по всем путям коллайдера
        {
            List<Vector2> _linePointsPositions = landCollider.GetPath(i).ToList();
            for (int j = 0; j < _linePointsPositions.Count; j++)
                _linePointsPositions[j] = landCollider.transform.TransformPoint(_linePointsPositions[j]);
            Line landLine = LineFromCollider(_linePointsPositions);
            
            for (int j = 0; j < landLine.points.Count; j++) // Надо проверить, что начальная точка снаружи
            {
                if (circleCollider.ClosestPoint(landLine.points[0].position) == landLine.points[0].position)
                {
                    ReorderList(landLine.points);
                    ReorderList(landLine.segments);
                }
                else 
                    break;
            }
            var result = Substraction(landLine, circleLine);
            allSplines.InsertRange(0, result);
        }
        landCollider.GetComponent<Land>().SetPath(allSplines);
    }

    private List<List<Point>> Substraction(Line landLine, Line circleLine)
    {
        for (int i = 0; i < circleLine.points.Count; i++) // Ставим дефолтные NextPoint для круга
        {
            int nextIndex = GetNext(i, circleLine.points.Count, false);
            circleLine.points[i].nextPoint = circleLine.points[nextIndex];
        }

        // Перебираем все сегменты, создаем точки пересечения
        for (int l = 0; l < landLine.segments.Count; l++)
        {
            Segment landSegment = landLine.segments[l];
            Vector2 al = landSegment.A.position;
            Vector2 bl = landSegment.B.position;
            // Смотрим какие сегменты круга пересекают этот сегмент
            // Тут надо учесть что пересечения может быть два
            for (int c = 0; c < circleLine.segments.Count; c++)
            {
                Segment circleSegment = circleLine.segments[c];
                Vector2 ac = circleLine.segments[c].A.position;
                Vector2 bc = circleLine.segments[c].B.position;

                if (Intersection.IsIntersecting(al, bl, ac, bc))
                {
                    Vector2 position = Intersection.GetIntersection(al, bl, ac, bc);
                    Point crossPoint = new Point();
                    crossPoint.position = position;
                    crossPoint.landSegment = landSegment;
                    crossPoint.circleSegment = circleSegment;
                    crossPoint.isCross = true;
                    landSegment.crossPoints.Add(crossPoint);
                    circleSegment.crossPoints.Add(crossPoint);
                }
            }
        }

        // Собираем новый массив точек с учетом пересечений
        RecalculateLine(landLine);
        RecalculateLine(circleLine);

        AllPoints(landLine, circleLine);
        return AllSplines(landLine);
    }

    private void AllPoints(Line landLine, Line circleLine)
    {
        List<Point> allPoints = new List<Point>(landLine.points);
        bool onLand = true;
        Point startPoint = allPoints[0];
        while (allPoints.Count > 0)
        {
            Point thePoint = allPoints[0];
            //смотрим находится ли точка снаружи
            if (circleCollider.ClosestPoint(thePoint.position) == thePoint.position || thePoint.isCross)
            {
                allPoints.RemoveAt(0);
                continue;
            }
            // Собираем точки по цепочке назначаем им NextPoint
            for (int i = 0; i < testIterations; i++)
            {
                Line currentLine;
                // ccw -- против часовой стрелки
                bool ccw;
                if (onLand)
                {
                    currentLine = landLine;
                    ccw = true;
                }
                else
                {
                    currentLine = circleLine;
                    ccw = false;
                }

                int currentIndex = currentLine.points.IndexOf(thePoint);
                int nextIndex = GetNext(currentIndex, currentLine.points.Count, ccw);
                thePoint.nextPoint = currentLine.points[nextIndex];
                allPoints.Remove(thePoint);
                if (thePoint.nextPoint.isCross) onLand = !onLand;
                thePoint = thePoint.nextPoint;
                if (startPoint == thePoint) break;
            }
        }
    }

    private List<List<Point>> AllSplines(Line landLine)
    {
        List<List<Point>> allSplines = new List<List<Point>>();
        List<Point> allPoints = new List<Point>(landLine.points);
        while (allPoints.Count > 0)
        {
            Point thePoint = allPoints[0];
            //смотрим находится ли точка снаружи
            if (circleCollider.ClosestPoint(thePoint.position) == thePoint.position || thePoint.isCross)
            {
                allPoints.RemoveAt(0);
                continue;
            }
            else
            {
                List<Point> newSpline = new List<Point>();
                allSplines.Add(newSpline);

                // Собираем точки по цепочке
                Point startPoint = thePoint;
                Point point = thePoint;

                newSpline.Add(point);
                allPoints.Remove(point);
                for (int i = 0; i < testIterations; i++)
                {
                    point = point.nextPoint;
                    if (point == startPoint) break;
                    newSpline.Add(point);
                    if (allPoints.Contains(point)) allPoints.Remove(point);
                }
            }
        }
        return allSplines;
    }

    private void RecalculateLine(Line line)
    {
        List<Point> newPoints = new List<Point>();
        for (int s = 0; s < line.segments.Count; s++)
        {
            Segment segment = line.segments[s];
            newPoints.Add(segment.A);
            if (segment.crossPoints.Count > 0)
            {
                segment.crossPoints.Sort((p1, p2) =>
                Vector3.Distance(segment.A.position, p1.position).
                    CompareTo(Vector3.Distance(segment.A.position, p2.position)));
            }
            newPoints.AddRange(segment.crossPoints);
        }
        line.points = newPoints;
    }

    // Сместьть все элементы списка на 1
    private void ReorderList<T>(List<T> list)
    {
        var first = list[0];
        for (int i = 0; i < list.Count; i++)
        {
            if (i == list.Count - 1) list[i] = first;
            else list[i] = list[i + 1];
        }
    }

    private Line LineFromCollider(List<Vector2> list)
    {
        Line line = new Line();
        List<Point> points = new List<Point>();
        List<Segment> segments = new List<Segment>();

        for (int i = 0; i < list.Count; i++)
        {
            Point point = new Point();
            point.position = list[i];
            points.Add(point);
        }

        for (int i = 0; i < list.Count; i++)
        {
            Segment segment = new Segment();
            segment.A = points[i];
            points[i].landSegment = segment;
            int bIndex = i + 1;
            if (bIndex >= list.Count) bIndex = 0;
            segment.B = points[bIndex];
            points[bIndex].circleSegment = segment;
            segments.Add(segment);
        }

        line.points = points;
        line.segments = segments;
        return line;
    }

    private int GetNext(int index, int length, bool isCCW)
    {
        int nextIndex = index + (isCCW ? 1 : -1);
        if (nextIndex >= length) nextIndex = 0;
        else if (nextIndex < 0) nextIndex = length - 1;
        return nextIndex;
    }
}
