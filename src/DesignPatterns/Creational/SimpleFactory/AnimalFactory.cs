namespace DesignPatterns.Creational.SimpleFactory;

// The factory – decides which IAnimal to create
public static class AnimalFactory
{
    public static IAnimal Create(string animalType) =>
        animalType.ToLower() switch
        {
            "dog"  => new Dog(),
            "cat"  => new Cat(),
            "bird" => new Bird(),
            _      => throw new ArgumentException($"Unknown animal: {animalType}")
        };
}
