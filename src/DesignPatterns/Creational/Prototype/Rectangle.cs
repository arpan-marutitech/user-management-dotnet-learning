namespace DesignPatterns.Creational.Prototype;

public class Rectangle : Shape
{
    public int Width  { get; set; }
    public int Height { get; set; }

    public override Shape Clone()
    {
        return (Rectangle)MemberwiseClone();
    }

    public override string ToString() =>
        $"Rectangle [Color={Color}, Width={Width}, Height={Height}]";
}
