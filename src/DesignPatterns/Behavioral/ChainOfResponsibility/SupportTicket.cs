namespace DesignPatterns.Behavioral.ChainOfResponsibility;

// The request object that travels along the chain
public class SupportTicket
{
    public string Issue    { get; init; } = string.Empty;
    public int    Priority { get; init; }   // 1=Low, 2=Medium, 3=High
}
