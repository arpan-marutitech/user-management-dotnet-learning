namespace DesignPatterns.Creational.Builder;

// The complex object being built
public class Pizza
{
    public string Crust    { get; set; } = string.Empty;
    public string Sauce    { get; set; } = string.Empty;
    public string Toppings { get; set; } = string.Empty;

    public override string ToString() =>
        $"Pizza [Crust={Crust}, Sauce={Sauce}, Toppings={Toppings}]";
}
