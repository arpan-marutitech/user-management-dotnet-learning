namespace DesignPatterns.Creational.FactoryMethod;

// Abstract creator – subclasses decide which ITransport to instantiate
public abstract class Logistics
{
    // The factory method
    protected abstract ITransport CreateTransport();

    // Business logic that uses the product
    public string PlanDelivery()
    {
        var transport = CreateTransport();
        return $"[FactoryMethod] {transport.Deliver()}";
    }
}

public class RoadLogistics : Logistics
{
    protected override ITransport CreateTransport() => new Truck();
}

public class SeaLogistics : Logistics
{
    protected override ITransport CreateTransport() => new Ship();
}
