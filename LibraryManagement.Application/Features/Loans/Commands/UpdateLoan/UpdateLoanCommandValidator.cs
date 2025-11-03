using FluentValidation;

namespace LibraryManagement.Application.Features.Loans.Commands.UpdateLoan
{
    public class UpdateLoanCommandValidator : AbstractValidator<UpdateLoanCommand>
    {
        public UpdateLoanCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.BookId).GreaterThan(0);
            RuleFor(x => x.MemberId).GreaterThan(0);
            RuleFor(x => x.LoanDate).NotEmpty();
            RuleFor(x => x.DueDate)
                .GreaterThan(x => x.LoanDate)
                .When(x => x.DueDate.HasValue);
        }
    }
}
