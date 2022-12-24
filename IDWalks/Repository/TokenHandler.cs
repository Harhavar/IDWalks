using IDWalks.Models.Domines;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IDWalks.Repository
{
    public interface ITokenHandler
    {
        Task<string> CreateToken(User user);
    }
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<string> CreateToken(User user)
        {
           


            //create claims
            var Claims = new List<Claim>();
            Claims.Add(new Claim(ClaimTypes.GivenName, user.FirstName));
            Claims.Add(new Claim(ClaimTypes.Surname, user.LastName));
            Claims.Add(new Claim(ClaimTypes.Email, user.Email));

            //Loop into Roles of user 
            user.Roles.ForEach((roles) =>
            {
                Claims.Add(new Claim(ClaimTypes.Role, roles));
            });

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var Credential = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials : Credential);

            return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(Token));
        }

       
    }
}
