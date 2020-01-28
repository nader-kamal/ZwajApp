using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.DTOS;
using ZwajApp.API.Models;

namespace ZwajApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository authRepository, IConfiguration config)
        {
            _config = config;
            _authRepository = authRepository;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //Validation
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();
            if (await _authRepository.UserExists(userForRegisterDto.UserName))
            {
                return BadRequest("هذا المستخدم مسجل من قبل");
            }
            var userToCreate = new User
            {
                Username = userForRegisterDto.UserName
            };
            var createdUser = await _authRepository.Register(userToCreate, userForRegisterDto.Password);
            return StatusCode(201);

        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _authRepository.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);
            if (userFromRepo == null)
            {
                return Unauthorized();
            }
            var claims = new[]{
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.Username)
            };
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:Token").Value));
            var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha512);
            var tokenDescriptor=new SecurityTokenDescriptor{
                Subject=new ClaimsIdentity(claims),
                Expires=DateTime.Now.AddDays(1),
                SigningCredentials=cred
            };
            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new {
                token=tokenHandler.WriteToken(token)
            });
        }
    }
}