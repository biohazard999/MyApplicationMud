using FluentValidation;
using HotChocolate.Subscriptions;
using MyApplicationMud.Shared.Validation;

namespace MyApplicationMud.ExternalApi.Queries;

public abstract record ValidationPayload<T>(T? Value = default)
{
    public IEnumerable<ValidationError> Errors { get; init; } = Enumerable.Empty<ValidationError>();
}

public record ValidationError(string PropertyName, string Message);

public record BookValidationPayload(Book? Value = default) : ValidationPayload<Book>(Value);

public class Mutations
{
    public class ServerBookInputModelValidator : BookInputModelValidator
    {
        public ServerBookInputModelValidator()
        {
            RuleFor(x => x.Title).MinimumLength(5);
            RuleFor(x => x.AuthorId).GreaterThanOrEqualTo(5);
        }
    }

    public async Task<BookValidationPayload> EditBook([ID(nameof(Book))] int bookId, BookModel book, [Service] ITopicEventSender sender)
    {
        var validator = new ServerBookInputModelValidator();
        var result = await validator.ValidateAsync(book);
        if (!result.IsValid)
        {
            var errors = result.Errors.Select(m => new ValidationError(m.PropertyName, m.ErrorMessage)).ToList();

            return new BookValidationPayload { Errors = errors };
        }

        var b = FakeData.books.First(m => m.Id == bookId);

        var index = FakeData.books.IndexOf(b);
        FakeData.books.RemoveAt(index);
        var editBook = new Book(bookId, book.Title, b.Image, FakeData.authors.First(m => m.Id == book.AuthorId));
        FakeData.books.Insert(index, editBook);

        await sender.SendAsync(nameof(Subscriptions.BookChanged), new BookChangedPayload(editBook, ChangeType.Modified));

        return new BookValidationPayload(editBook);
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

        await sender.SendAsync(nameof(Subscriptions.BookChanged), new BookChangedPayload(bookDeleted, ChangeType.Deleted));

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
