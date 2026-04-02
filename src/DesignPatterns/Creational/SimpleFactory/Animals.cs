namespace DesignPatterns.Creational.SimpleFactory;

public class Dog : IAnimal
{
    public string Speak() => "Woof!";
}

public class Cat : IAnimal
{
    public string Speak() => "Meow!";
}

public class Bird : IAnimal
{
    public string Speak() => "Tweet!";
}
