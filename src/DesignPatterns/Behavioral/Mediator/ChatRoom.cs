namespace DesignPatterns.Behavioral.Mediator;

// Concrete mediator – routes messages to all users except the sender
public class ChatRoom : IChatMediator
{
    private readonly List<ChatUser> _users = new();

    public void Register(ChatUser user) => _users.Add(user);

    public void SendMessage(string message, ChatUser sender)
    {
        Console.WriteLine($"  {sender.Name} says: \"{message}\"");
        foreach (var user in _users)
        {
            if (user != sender)
                user.Receive(message);
        }
    }
}
