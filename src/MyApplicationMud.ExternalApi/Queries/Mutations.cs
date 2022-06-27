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

        await sender.SendAsync(nameof(Subscriptions.BookChanged), new BookChangedPayload(editBook, ChangeType.Modified));

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

        await sender.SendAsync(nameof(Subscriptions.BookChanged), new BookChangedPayload(newBook, ChangeType.Added));

        return newBook;
    }

    public async Task<bool> DeleteBook([ID(nameof(Book))] int bookId, [Service] ITopicEventSender sender)
    {
        var bookDeleted = FakeData.books.First(m => m.Id == bookId);

        FakeData.books.Remove(bookDeleted);

        await sender.SendAsync(nameof(Subscriptions.BookChanged), new BookChangedPayload(bookDeleted, ChangeType.Modified));

        return true;
    }

    public async Task<Book> SetBookImage([ID(nameof(Book))] int bookId, IFile file, [Service] ITopicEventSender sender)
    {
        var book = FakeData.books.First(m => m.Id == bookId);

        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var format = "image/png";
        var imageDataUrl = $"data:{format};base64,{Convert.ToBase64String(ms.ToArray())}";

        var index = FakeData.books.IndexOf(book);
        FakeData.books.RemoveAt(index);
        var editBook = book with { Image = imageDataUrl };
        FakeData.books.Insert(index, editBook);

        await sender.SendAsync(nameof(Subscriptions.BookChanged), new BookChangedPayload(editBook, ChangeType.Modified));

        return editBook;
    }
}
