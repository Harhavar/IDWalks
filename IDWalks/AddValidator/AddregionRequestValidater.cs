using FluentValidation;
using IDWalks.Models.DTO;

namespace IDWalks.AddValidator
{
    public class AddregionRequestValidater : AbstractValidator<Models.DTO.AddRegionRequest >
    {
        public AddregionRequestValidater()
        {
            RuleFor( s => s.Code).NotEmpty();
            RuleFor( s => s.Name).NotEmpty();
            RuleFor( s => s.Area).GreaterThan(0);
            RuleFor( s => s.Population).GreaterThanOrEqualTo(0);

        }
    }
}
