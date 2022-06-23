using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;

namespace MyApplicationMud.ExternalApi.Queries;

public class Query
{
    public UserInfo GetUser(ClaimsPrincipal claimsPrincipal)
    {
        var name = claimsPrincipal.Identity?.Name ?? "Anonymer Benutzer";
        var claims = claimsPrincipal.Claims.Select(c => new UserClaim(c.Type, c.Value)).ToList();
        return new(name, claims);
    }

    public Book GetBook() 
        => new Book("C# in depth", new("Jon Skeet"));
}

public record Book(string Title, Author Author);
public record Author(string Name);

public record UserInfo(string Name, IList<UserClaim> Claims);
[Authorize]
public record UserClaim(string Key, string Value);