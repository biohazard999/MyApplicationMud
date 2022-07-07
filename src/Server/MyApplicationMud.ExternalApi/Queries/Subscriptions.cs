namespace MyApplicationMud.ExternalApi.Queries;

public class Subscriptions
{
    [Subscribe]
    public BookChangedPayload BookChanged([EventMessage] BookChangedPayload book) => book;
}

public record BookChangedPayload(Book book, ChangeType Type);

public enum ChangeType
{
    Added,
    Modified,
    Deleted
}
