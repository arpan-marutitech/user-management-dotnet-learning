namespace DesignPatterns.Creational.Prototype;

public class Circle : Shape
{
    public int Radius { get; set; }

    public override Shape Clone()
    {
        // Shallow copy using MemberwiseClone
        return (Circle)MemberwiseClone();
    }

    public override string ToString() =>
        $"Circle [Color={Color}, Radius={Radius}]";
}
