using System.Collections.ObjectModel;

namespace Patterns.Adapter;

public class Point
{
    public int X, Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
    }
}

public class Line
{
    public Point Start, End;

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }
}

public abstract class VectorObject 
    : Collection<Line> {}

public class VectorRectangle : VectorObject
{
    public VectorRectangle(int x, int y, int width, int height)
    {
        Add(new Line(new Point(x,y), new Point(x+width, y) ));
        Add(new Line(new Point(x+width,y), new Point(x+width, y+height) ));
        Add(new Line(new Point(x,y), new Point(x, y+height) ));
        Add(new Line(new Point(x,y+height), new Point(x+width, y+height) ));
    }
}

public class LineToPointAdapter : Collection<Point>
{
    private static int count = 0;

    public LineToPointAdapter(Line line)
    {
        Console.WriteLine($"{++count}: Generating points for line" 
                  + $" [{line.Start.X},{line.Start.Y}]-" 
                  + $"[{line.End.X},{line.End.Y}] (no caching)");

        int left = Math.Min(line.Start.X, line.End.X);
        int right = Math.Max(line.Start.X, line.End.X);
        int top = Math.Min(line.Start.Y, line.End.Y);
        int bottom = Math.Max(line.Start.Y, line.End.Y);

        if (right - left == 0)
        {
            for (int y = top; y <= bottom; ++y)
            {
                Add(new Point(left, y));
            }
        } else if (line.End.Y - line.Start.Y == 0)
        {
            for (int x = left; x <= right; ++x)
            {
                Add(new Point(x, top));
            }
        }
    }
}