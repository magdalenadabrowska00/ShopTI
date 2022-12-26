using FluentValidation;
using ShopTI.Entities;

namespace ShopTI.Models.Validators
{
    public class RegisterUserValidator : AbstractValidator<RegisterUser>
    {
        public RegisterUserValidator(ShopDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Custom((value, context) =>
            {
                var emailInUse = dbContext.Users.Any(x => x.Email == value);
                if (emailInUse)
                {
                    context.AddFailure("Email", "Ten email jest zajęty.");
                }
            });

            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Hasło jest wymagane");
            RuleFor(x => x.ConfirmPassword).NotEmpty().Equal(x => x.Password).WithMessage("Hasło potwierdzające musi być takie jakie podano wcześniej!");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("To pole jest wymagane.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("To pole jest wymagane.");
            RuleFor(x => x.Country).NotEmpty().WithMessage("To pole jest wymagane.");
            RuleFor(x => x.City).NotEmpty().WithMessage("To pole jest wymagane.");
            RuleFor(x => x.PostalCode).NotEmpty().WithMessage("To pole jest wymagane.");
            RuleFor(x => x.Street).NotEmpty().WithMessage("To pole jest wymagane.");
        }
    }
}
