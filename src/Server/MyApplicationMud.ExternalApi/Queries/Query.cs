using HotChocolate.AspNetCore.Authorization;

using MyApplicationMud.Shared.Validation;

using System.Security.Claims;

namespace MyApplicationMud.ExternalApi.Queries;

public class Query
{
    public ServerInfo GetServerInfo() => new(DateTime.UtcNow);

    public UserInfo GetUser(ClaimsPrincipal claimsPrincipal)
    {
        var name = claimsPrincipal.Identity?.Name ?? "Anonymer Benutzer";
        var claims = claimsPrincipal.Claims.Select(c => new UserClaim(c.Type, c.Value)).ToList();
        return new(name, claims);
    }

    public Book GetBook(int id)
        => FakeData.books.First(b => b.Id == id);

    //[UsePaging]
    //[UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Book> GetBooks()
        => FakeData.books.AsQueryable();

    public IQueryable<Author> GetAuthors()
        => FakeData.authors.AsQueryable();
}

public record BookModel(string Title, [ID(nameof(Author))] int AuthorId) : IBookInputModel;

public record ServerInfo([GraphQLDescription("Returns the server time in UTC")] DateTime ServerTime);

public record Book([ID] int Id, string Title, string Image, Author Author);

public record Author([ID] int Id, string Name);

public record UserInfo(string Name, IList<UserClaim> Claims);
[Authorize]
public record UserClaim(string Key, string Value);
