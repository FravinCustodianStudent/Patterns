namespace Patterns.Prototype;

public class Point
{
    public int X, Y;

    public Point(int x,int y)
    {
        X = x;
        Y = y;
    }
}
public class PointFabric
{
    public static Point NewCartesianPoint(int x,int y)
    {
        return new Point(x,y);
    }
}
public class Line
{
    public Point Start, End;

    public Line(Point start,Point end)
    {
        Start = start;
        End = end;
    }
}

public static class Extensions
{
    public static Line DeepCopy(this Line self)
    {
        
        var newLine = new Line(
            PointFabric.NewCartesianPoint(self.Start.X, self.Start.Y),
            PointFabric.NewCartesianPoint(self.Start.X, self.Start.Y));
        return newLine;
    }
}