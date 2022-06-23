namespace MyApplicationMud.ExternalApi.Queries;

public class Query
{
    public Book GetBook() 
        => new Book("C# in depth", new("Jon Skeet"));
}

public record Book(string Title, Author Author);
public record Author(string Name);