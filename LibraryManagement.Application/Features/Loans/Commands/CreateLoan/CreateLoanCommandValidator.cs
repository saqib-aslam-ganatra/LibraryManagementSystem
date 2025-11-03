using FluentValidation;

namespace LibraryManagement.Application.Features.Loans.Commands.CreateLoan
{
    public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
    {
        public CreateLoanCommandValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0);
            RuleFor(x => x.MemberId).GreaterThan(0);
            RuleFor(x => x.DueDate)
                .GreaterThan(x => x.LoanDate ?? DateTime.UtcNow)
                .When(x => x.DueDate.HasValue);
        }
    }
}
