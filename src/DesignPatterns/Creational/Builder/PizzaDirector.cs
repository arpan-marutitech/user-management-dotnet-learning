namespace DesignPatterns.Creational.Builder;

// Director – knows how to build specific pizza recipes
public class PizzaDirector
{
    private readonly IPizzaBuilder _builder;

    public PizzaDirector(IPizzaBuilder builder) => _builder = builder;

    public Pizza MakeMargherita() =>
        _builder.SetCrust("Thin").SetSauce("Tomato").AddToppings("Mozzarella, Basil").Build();

    public Pizza MakePepperoni() =>
        _builder.SetCrust("Thick").SetSauce("Spicy Tomato").AddToppings("Pepperoni, Mozzarella").Build();
}
