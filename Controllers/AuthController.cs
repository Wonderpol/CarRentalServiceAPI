using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.User;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Responses;
using CarRentalRestApi.Repository;
using CarRentalRestApi.Services.AuthService;
using CarRentalRestApi.Utils.AuthUtils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalRestApi.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class UserController: ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register([FromBody]UserRegisterDto request)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            
            var response = await _authService.Register(
                new User
                {
                    Email = request.Email, 
                    FirstName = request.FirstName, 
                    LastName = request.LastName
                }, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }
        
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody]UserLoginDto request)
        {
            var response = await _authService.Login(
                request.Email, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginResponse>> RefreshToken([FromBody]RefreshTokenDto refreshToken)
        {
            var response = await _authService.RefreshToken(refreshToken.Token);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("GetMe")]
        [Authorize]
        public async Task<ActionResult<ServiceResponse<UserGetDto>>> GetMe()
        {
            var id = int.Parse(User.Claims.First(cla => cla.Type == ClaimTypes.NameIdentifier).Value);
            var response = await _authService.GetMe(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);

        }

    }
}