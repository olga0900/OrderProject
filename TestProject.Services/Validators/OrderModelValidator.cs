using FluentValidation;
using TestProject.Services.Contracts.Models;

namespace TestProject.Services.Validators
{
    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            RuleFor(x => x.SenderAddress)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(10, 200).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.SenderCity)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(2, 40).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.RecipientAddress)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(10, 200).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.RecipientCity)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .NotNull().WithMessage(MessageForValidation.DefaultMessage)
               .Length(2, 40).WithMessage(MessageForValidation.LengthMessage);

            RuleFor(x => x.Weight)
               .InclusiveBetween(10, 100000).WithMessage(MessageForValidation.InclusiveBetweenMessage);

            RuleFor(x => x.PickupDate)
               .NotEmpty().WithMessage(MessageForValidation.DefaultMessage)
               .GreaterThan(DateTimeOffset.UtcNow).WithMessage(MessageForValidation.InclusiveBetweenMessage);
        }
    }
}
