using DesignPatterns.Creational.SimpleFactory;
using DesignPatterns.Creational.FactoryMethod;
using DesignPatterns.Creational.AbstractFactory;
using DesignPatterns.Creational.Builder;
using DesignPatterns.Creational.Prototype;
using DesignPatterns.Creational.Singleton;
using DesignPatterns.Architectural.Repository;
using DesignPatterns.Behavioral.ChainOfResponsibility;
using DesignPatterns.Behavioral.Mediator;

static void Section(string title)
{
    Console.WriteLine();
    Console.WriteLine(new string('─', 50));
    Console.WriteLine($"  {title}");
    Console.WriteLine(new string('─', 50));
}

// ─────────────────────────────────────────────────────────
// 1. SIMPLE FACTORY
// ─────────────────────────────────────────────────────────
Section("1. Simple Factory");
// A single factory class creates objects by type string.
// Caller does not need to know the concrete class.
var dog  = AnimalFactory.Create("dog");
var cat  = AnimalFactory.Create("cat");
var bird = AnimalFactory.Create("bird");
Console.WriteLine($"Dog  says: {dog.Speak()}");
Console.WriteLine($"Cat  says: {cat.Speak()}");
Console.WriteLine($"Bird says: {bird.Speak()}");

// ─────────────────────────────────────────────────────────
// 2. FACTORY METHOD
// ─────────────────────────────────────────────────────────
Section("2. Factory Method");
// The creator class has an abstract CreateTransport() method.
// Subclasses override it to return a specific product.
Logistics road = new RoadLogistics();
Logistics sea  = new SeaLogistics();
Console.WriteLine(road.PlanDelivery());
Console.WriteLine(sea.PlanDelivery());

// ─────────────────────────────────────────────────────────
// 3. ABSTRACT FACTORY
// ─────────────────────────────────────────────────────────
Section("3. Abstract Factory");
// A factory that creates *families* of related objects.
// Swap the factory to switch the entire UI theme.
IUIFactory winFactory = new WindowsFactory();
Console.WriteLine($"Windows: {winFactory.CreateButton().Render()} + {winFactory.CreateCheckbox().Render()}");

IUIFactory macFactory = new MacFactory();
Console.WriteLine($"Mac:     {macFactory.CreateButton().Render()} + {macFactory.CreateCheckbox().Render()}");

// ─────────────────────────────────────────────────────────
// 4. BUILDER
// ─────────────────────────────────────────────────────────
Section("4. Builder");
// Separate the construction of a complex object from its representation.
// The Director uses the builder step-by-step to assemble a specific variant.
var director = new PizzaDirector(new PizzaBuilder());
Console.WriteLine(director.MakeMargherita());
Console.WriteLine(director.MakePepperoni());

// You can also use the builder directly without a director:
var customPizza = new PizzaBuilder()
    .SetCrust("Stuffed")
    .SetSauce("BBQ")
    .AddToppings("Chicken, Onions")
    .Build();
Console.WriteLine($"Custom: {customPizza}");

// ─────────────────────────────────────────────────────────
// 5. PROTOTYPE
// ─────────────────────────────────────────────────────────
Section("5. Prototype");
// Clone an existing object instead of building a new one from scratch.
var original = new Circle { Color = "Red", Radius = 10 };
var cloned   = (Circle)original.Clone();
cloned.Color  = "Blue";          // change only the clone
cloned.Radius = 20;

Console.WriteLine($"Original : {original}");
Console.WriteLine($"Clone    : {cloned}");

var rect1 = new Rectangle { Color = "Green", Width = 5, Height = 3 };
var rect2 = (Rectangle)rect1.Clone();
rect2.Width = 10;
Console.WriteLine($"Rect orig: {rect1}");
Console.WriteLine($"Rect copy: {rect2}");

// ─────────────────────────────────────────────────────────
// 6. SINGLETON
// ─────────────────────────────────────────────────────────
Section("6. Singleton");
// Ensures only one instance exists. Both variables point to the same object.
var settings1 = AppSettings.Instance;
var settings2 = AppSettings.Instance;
Console.WriteLine($"Same instance? {ReferenceEquals(settings1, settings2)}");
Console.WriteLine($"App: {settings1.AppName}, MaxRetries: {settings1.MaxRetries}");
settings1.MaxRetries = 5;
Console.WriteLine($"After change via settings1 – settings2.MaxRetries = {settings2.MaxRetries}");

// ─────────────────────────────────────────────────────────
// 7. REPOSITORY
// ─────────────────────────────────────────────────────────
Section("7. Repository");
// Repository hides storage details behind a small data-access contract.
IStudentRepository studentRepository = new StudentRepository();
studentRepository.Add(new Student { Id = 1, Name = "Arjun", Email = "arjun@example.com" });
studentRepository.Add(new Student { Id = 2, Name = "Meera", Email = "meera@example.com" });

Console.WriteLine("All students:");
foreach (var student in studentRepository.GetAll())
    Console.WriteLine($"  {student}");

studentRepository.Update(new Student { Id = 2, Name = "Meera Patel", Email = "meera.patel@example.com" });
Console.WriteLine($"Updated student: {studentRepository.GetById(2)}");

studentRepository.Delete(1);
Console.WriteLine("After delete:");
foreach (var student in studentRepository.GetAll())
    Console.WriteLine($"  {student}");

// ─────────────────────────────────────────────────────────
// 8. CHAIN OF RESPONSIBILITY
// ─────────────────────────────────────────────────────────
Section("8. Chain of Responsibility");
// Build the chain: Level1 → Level2 → Level3
var level1 = new Level1Support();
var level2 = new Level2Support();
var level3 = new Level3Support();
level1.SetNext(level2).SetNext(level3);

// Each ticket is passed to level1 and travels until handled.
var tickets = new[]
{
    new SupportTicket { Issue = "Slow internet",        Priority = 1 },
    new SupportTicket { Issue = "Server disk full",     Priority = 2 },
    new SupportTicket { Issue = "Data breach detected", Priority = 3 },
};
foreach (var ticket in tickets)
    Console.WriteLine(level1.Handle(ticket));

// ─────────────────────────────────────────────────────────
// 9. MEDIATOR
// ─────────────────────────────────────────────────────────
Section("9. Mediator");
// Users never talk to each other directly – all messages go through ChatRoom.
var chatRoom = new ChatRoom();
var alice = new ChatUser("Alice", chatRoom);
var bob   = new ChatUser("Bob",   chatRoom);
var carol = new ChatUser("Carol", chatRoom);

alice.Send("Hello everyone!");
bob.Send("Hi Alice!");

Console.WriteLine();
