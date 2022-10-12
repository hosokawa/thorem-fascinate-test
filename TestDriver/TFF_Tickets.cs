using Xunit;

namespace TestDriver;

public class TFF_Tickets
{
    [Fact]
    public void Test_TFF_64()
    {
        TffTicket64 ticket = new TffTicket64();
        Assert.True(ticket.execute() > 0);
        Assert.True(ticket.executeGuest() > 0);
        Assert.True(ticket.executeWithLogin() > 0);
    }
}