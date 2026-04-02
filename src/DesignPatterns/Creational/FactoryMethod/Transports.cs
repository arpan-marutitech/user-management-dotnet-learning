namespace DesignPatterns.Creational.FactoryMethod;

// Concrete products
public class Truck : ITransport
{
    public string Deliver() => "Delivered by Truck on road.";
}

public class Ship : ITransport
{
    public string Deliver() => "Delivered by Ship over sea.";
}
