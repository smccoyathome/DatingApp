using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo,
               IConfiguration config, 
               IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
            _config = config;
        }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserForRegistration userForRegistration)
    {
        //request validated by DTO

        userForRegistration.Username = userForRegistration.Username.ToLower();

        if (await _repo.UserExists(userForRegistration.Username))
            return BadRequest("Username already exits");

        var userToCreate = new User
        {
            Username = userForRegistration.Username
        };

        var createdUser = await _repo.Register(userToCreate, userForRegistration.Password);

        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserForLogin userForLogin)
    {
        var userFromRepo = await _repo.Login(userForLogin.Username, userForLogin.Password);

        if (userFromRepo == null)
            return Unauthorized();

        var claims = new[]
        {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

        var key = new SymmetricSecurityKey(Encoding.UTF8
                 .GetBytes(_config.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var user = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user
            });

    }
}
}