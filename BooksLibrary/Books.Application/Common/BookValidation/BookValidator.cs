using Books.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Application.Common.BookValidation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(book => book.ISBN)
                .NotEmpty().WithMessage("ISBN cannot be empty")
                .Length(8).WithMessage("ISBN must be 8 characters");

            RuleFor(book => book.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters");

            RuleFor(book => book.Genre)
                .NotEmpty().WithMessage("Genre is required");

            RuleFor(book => book.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters");

            RuleFor(book => book.AuthorId)
                .GreaterThan(0).WithMessage("AuthorId must be a positive integer");

            RuleFor(book => book.StartTimeOfBorrowingBook)
                .LessThan(book => book.EndTimeOfBorrowingBook)
                .When(book => book.EndTimeOfBorrowingBook != default)
                .WithMessage("The start time must be earlier than the end time.");
        }
    }
}
