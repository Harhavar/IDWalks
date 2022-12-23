using FluentValidation;

namespace IDWalks.AddValidator
{
    public class AddwalkValidator : AbstractValidator<Models.DTO.AddWalkRequest>
    {
        public AddwalkValidator()
        {
            RuleFor(x => x.name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);

        }
    }
}
