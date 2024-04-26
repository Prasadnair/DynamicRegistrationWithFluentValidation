using FluentValidation;

namespace Domain
{
    public class PersonValidation:AbstractValidator<Person>
    {
        public PersonValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Age).GreaterThan(0).WithMessage("Age must be greater than 18");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");
        }
    }
}
