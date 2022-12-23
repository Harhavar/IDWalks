using FluentValidation;

namespace IDWalks.AddValidator
{
    public class Updatevalidator : AbstractValidator<Models.DTO.UpdateWalkRequest>
    {
        public Updatevalidator()
        {
            RuleFor(x => x.name).NotEmpty();
            RuleFor(x => x.Length).GreaterThan(0);
        }
    }
}
