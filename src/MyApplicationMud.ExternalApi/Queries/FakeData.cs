using Bogus;

namespace MyApplicationMud.ExternalApi.Queries;

public class FakeData
{
    //Repeatable values for bogus
    static FakeData() => Randomizer.Seed = new Random(8675309);

    internal static Faker<Author> authorFaker = new Faker<Author>()
         .CustomInstantiator(f =>
            new Author(
                f.UniqueIndex,
                f.Name.FullName()
            )
        );

    internal static Faker<Book> bookFaker = new Faker<Book>()
        .CustomInstantiator(f =>
            new Book(
                f.UniqueIndex,
                f.Hacker.Phrase(),
                f.Image.PicsumUrl(width: 480, height: 640),
                f.PickRandom(authors)
            )
        );

    internal static IList<Author> authors = authorFaker.Generate(10).ToList();

    internal static IList<Book> books = bookFaker.Generate(50).ToList();
}
