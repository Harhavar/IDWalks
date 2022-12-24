using IDWalks.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IDWalks.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository , ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAync(Models.DTO.LoginRequest loginRequest)
        {
            //verify the incoming request 
            //if (loginRequest == null)
            //{
            //    return BadRequest();

            //}
            //check user is authenticated 
            // check user name and password 
           var user = await userRepository.AuthenticateAsync(loginRequest.username, loginRequest.password);

            if (user != null)
            {
                //generate token 
                var Token = await tokenHandler.CreateToken(user);
                return Ok(Token);

            }

            return BadRequest("user name or password is incurrect");
        }
    }
}
