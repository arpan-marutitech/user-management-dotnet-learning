namespace DesignPatterns.Creational.Builder;

// Concrete builder
public class PizzaBuilder : IPizzaBuilder
{
    private readonly Pizza _pizza = new();

    public IPizzaBuilder SetCrust(string crust)      { _pizza.Crust    = crust;    return this; }
    public IPizzaBuilder SetSauce(string sauce)      { _pizza.Sauce    = sauce;    return this; }
    public IPizzaBuilder AddToppings(string toppings){ _pizza.Toppings = toppings; return this; }
    public Pizza Build() => _pizza;
}
