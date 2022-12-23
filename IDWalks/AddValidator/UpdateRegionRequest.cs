using FluentValidation;
using IDWalks.Models.DTO;

namespace IDWalks.AddValidator
{
    public class UpdateRegionRequest : AbstractValidator<Models.DTO.UpdateRegionRequest >
    {
        public UpdateRegionRequest()
        {
            RuleFor(s => s.Code).NotEmpty();
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.Area).GreaterThan(0);
            RuleFor(s => s.Population).GreaterThanOrEqualTo(0);

        }
    }
}
