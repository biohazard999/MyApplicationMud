namespace MyApplicationMud.ExternalApi.Queries;

public class Subscriptions
{
    [Subscribe]
    public Book BookChanged([EventMessage] Book book) => book;

    [Subscribe]
    public Book BookAdded([EventMessage] Book book) => book;

    [Subscribe]
    public Book BookDeleted([EventMessage] Book book) => book;
}
