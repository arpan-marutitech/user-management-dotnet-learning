namespace DesignPatterns.Behavioral.ChainOfResponsibility;

// Handler interface
public interface ITicketHandler
{
    ITicketHandler SetNext(ITicketHandler next);
    string Handle(SupportTicket ticket);
}

// Abstract base handler – stores the next handler and delegates when needed
public abstract class BaseTicketHandler : ITicketHandler
{
    private ITicketHandler? _next;

    public ITicketHandler SetNext(ITicketHandler next)
    {
        _next = next;
        return next;   // allows chaining: h1.SetNext(h2).SetNext(h3)
    }

    public virtual string Handle(SupportTicket ticket)
    {
        if (_next is not null)
            return _next.Handle(ticket);

        return $"[CoR] Ticket '{ticket.Issue}' could not be resolved.";
    }
}
