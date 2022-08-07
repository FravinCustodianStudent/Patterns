namespace Patterns.Factory;

public class Point
{
    private double _x, _y;

    private Point(double x,double y)
    {
        _x = x;
        _y = y;
    }

    public static Point NewCartesianPoint(double x,double y)
    {
        return new Point(x,y);
    }
    public static Point NewPolarPoint(double rho,double theta)
    {
        return new Point(rho*Math.Cos(theta),rho*Math.Sin(theta));
    }
}
