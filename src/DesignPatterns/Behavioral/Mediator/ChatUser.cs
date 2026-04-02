namespace DesignPatterns.Behavioral.Mediator;

// Colleague – a chat user that communicates only through the mediator
public class ChatUser
{
    private readonly IChatMediator _mediator;
    public string Name { get; }

    public ChatUser(string name, IChatMediator mediator)
    {
        Name      = name;
        _mediator = mediator;
        _mediator.Register(this);
    }

    // Send a message via the mediator
    public void Send(string message) =>
        _mediator.SendMessage(message, this);

    // Receive a message (called by the mediator)
    public void Receive(string message) =>
        Console.WriteLine($"  [{Name}] received: {message}");
}
