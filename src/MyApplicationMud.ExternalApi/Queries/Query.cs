﻿using HotChocolate.AspNetCore.Authorization;
using System.Security.Claims;
using Bogus;

namespace MyApplicationMud.ExternalApi.Queries;

public class FakeData
{
    //Repeatable values for bogus
    static FakeData() => Randomizer.Seed = new Random(8675309);

    static Faker<Author> authorFaker = new Faker<Author>()
         .CustomInstantiator(f =>
            new Author(
                f.UniqueIndex,
                f.Name.FullName()
            )
        );

    static Faker<Book> bookFaker = new Faker<Book>()
        .CustomInstantiator(f =>
            new Book(
                f.UniqueIndex,
                f.Hacker.Phrase(),
                f.PickRandom(authors)
            )
        );

    internal static IList<Author> authors = new List<Author>()
    {
        new(1, "Jon Skeet"),
    }.Concat(authorFaker.Generate(10)).ToList();

    internal static IList<Book> books = new List<Book>()
    {
        new Book(1, "C# in depth", new(1, "Jon Skeet")),
    }.Concat(bookFaker.Generate(100)).ToList();
}

public class Query
{
    public ServerInfo GetServerInfo() => new(DateTime.UtcNow);

    public UserInfo GetUser(ClaimsPrincipal claimsPrincipal)
    {
        var name = claimsPrincipal.Identity?.Name ?? "Anonymer Benutzer";
        var claims = claimsPrincipal.Claims.Select(c => new UserClaim(c.Type, c.Value)).ToList();
        return new(name, claims);
    }

    
    public Book GetBook() 
        => new Book(1, "C# in depth", new(1, "Jon Skeet"));

    [UseFiltering]
    public IQueryable<Book> GetBooks()
        => FakeData.books.AsQueryable();
}

public record ServerInfo([GraphQLDescription("Returns the server time in UTC")] DateTime ServerTime);

public record Book([ID]int Id, string Title, Author Author);
public record Author([ID] int Id, string Name);

public record UserInfo(string Name, IList<UserClaim> Claims);
[Authorize]
public record UserClaim(string Key, string Value);