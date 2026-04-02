namespace DesignPatterns.Behavioral.ChainOfResponsibility;

// Level 1: handles only Low priority (1)
public class Level1Support : BaseTicketHandler
{
    public override string Handle(SupportTicket ticket)
    {
        if (ticket.Priority == 1)
            return $"[CoR] Level-1 resolved '{ticket.Issue}' (Low priority).";

        return base.Handle(ticket);   // pass up the chain
    }
}

// Level 2: handles Medium priority (2)
public class Level2Support : BaseTicketHandler
{
    public override string Handle(SupportTicket ticket)
    {
        if (ticket.Priority == 2)
            return $"[CoR] Level-2 resolved '{ticket.Issue}' (Medium priority).";

        return base.Handle(ticket);
    }
}

// Level 3: handles High priority (3)
public class Level3Support : BaseTicketHandler
{
    public override string Handle(SupportTicket ticket)
    {
        if (ticket.Priority == 3)
            return $"[CoR] Level-3 resolved '{ticket.Issue}' (High priority).";

        return base.Handle(ticket);
    }
}
