namespace DesignPatterns.Creational.AbstractFactory;

// The abstract factory interface – creates a family of related products
public interface IUIFactory
{
    IButton   CreateButton();
    ICheckbox CreateCheckbox();
}
