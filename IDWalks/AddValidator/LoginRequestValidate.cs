using FluentValidation;

namespace IDWalks.AddValidator
{
    public class LoginRequestValidate : AbstractValidator<Models.DTO.LoginRequest>
    {
        public LoginRequestValidate()
        {
               RuleFor( x => x.username).NotEmpty();
               RuleFor( x => x.password).NotEmpty();
               
        }
    }
}
