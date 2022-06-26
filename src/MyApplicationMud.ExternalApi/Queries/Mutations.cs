using HotChocolate.Subscriptions;

namespace MyApplicationMud.ExternalApi.Queries;

public class Mutations
{
    public async Task<Book> EditBook([ID(nameof(Book))] int bookId, BookModel book, [Service] ITopicEventSender sender)
    {
        var b = FakeData.books.First(m => m.Id == bookId);

        var index = FakeData.books.IndexOf(b);
        FakeData.books.RemoveAt(index);
        var editBook = new Book(bookId, book.Title, b.Image, FakeData.authors.First(m => m.Id == book.AuthorId));
        FakeData.books.Insert(index, editBook);

        await sender.SendAsync(nameof(Subscriptions.BookChanged), editBook);

        return editBook;
    }

    public async Task<Book> AddBook(BookModel book, [Service] ITopicEventSender sender)
    {
        var newBook = FakeData.bookFaker.Generate() with
        {
            Title = book.Title,
            Author = FakeData.authors.First(m => m.Id == book.AuthorId)
        };
        
        FakeData.books.Add(newBook);

        await sender.SendAsync(nameof(Subscriptions.BookAdded), newBook);

        return newBook;
    }

    public async Task<bool> DeleteBook([ID(nameof(Book))]int bookId, [Service] ITopicEventSender sender)
    {
        var bookDeleted = FakeData.books.First(m => m.Id == bookId);

        FakeData.books.Remove(bookDeleted);

        await sender.SendAsync(nameof(Subscriptions.BookDeleted), bookDeleted);

        return true;
    }
}
