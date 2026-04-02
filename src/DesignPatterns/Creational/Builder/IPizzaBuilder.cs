namespace DesignPatterns.Creational.Builder;

// Builder interface – defines all the steps
public interface IPizzaBuilder
{
    IPizzaBuilder SetCrust(string crust);
    IPizzaBuilder SetSauce(string sauce);
    IPizzaBuilder AddToppings(string toppings);
    Pizza Build();
}
