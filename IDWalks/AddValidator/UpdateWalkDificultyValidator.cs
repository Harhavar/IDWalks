using FluentValidation;

namespace IDWalks.AddValidator
{
    public class UpdateWalkDificultyValidator : AbstractValidator<Models.DTO.UpdateWalkDificulty>
    {
        public UpdateWalkDificultyValidator()
        {
            RuleFor(x => x.code).NotEmpty();
        }
    }
}
