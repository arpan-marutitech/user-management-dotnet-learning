namespace DesignPatterns.Creational.Prototype;

// Base class with a Clone method
public abstract class Shape
{
    public string Color { get; set; } = "Black";

    // Each subclass returns a copy of itself
    public abstract Shape Clone();
}
