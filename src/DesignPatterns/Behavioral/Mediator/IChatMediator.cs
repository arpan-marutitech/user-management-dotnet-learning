namespace DesignPatterns.Behavioral.Mediator;

// Mediator interface – defines how components communicate
public interface IChatMediator
{
    void SendMessage(string message, ChatUser sender);
    void Register(ChatUser user);
}
