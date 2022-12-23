using FluentValidation;

namespace IDWalks.AddValidator
{
    public class AddwalkDificultyValidator : AbstractValidator<Models.DTO.AddWalkDificultyRequest>
    {
        public AddwalkDificultyValidator()
        {
            RuleFor(x => x.code).NotEmpty();
        }
    }
}
