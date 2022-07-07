using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace MyApplicationMud.Shared.Validation;

public interface IBookInputModel
{
    public string Title { get; }
    public int AuthorId { get; }
}

public class BookInputModelValidator : AbstractValidator<IBookInputModel>
{
    public BookInputModelValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.AuthorId).GreaterThanOrEqualTo(1);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<IBookInputModel>.CreateWithOptions((IBookInputModel)model, x => x.IncludeProperties(propertyName)));

        if (result.IsValid)
        {
            return Array.Empty<string>();
        }

        return result.Errors.Select(e => e.ErrorMessage);
    };
}
