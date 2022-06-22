namespace MyApplicationMud.ExternalApi.Queries;

public class Query
{
    public Book GetBook() 
        => new Book("C# in depth", "Jon Skeet");
}

public record Book(string Title, string Author);