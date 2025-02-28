using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using System.Xml.XPath;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using QuizApp.Data.DTO;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace QuizApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserController(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userRepository.ValidateUser(loginDTO.Email, loginDTO.Password);

            if (user != null) 
            {
                var token = GenerateJwtToken(user);

                return Ok(new { Id = user.Id, name = user.Name, role = user.Role, Token = token });

            }

            return Unauthorized();
        }
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok();
        }

        [HttpGet("session-data")]
        [Authorize]
        public IActionResult GetSessionData()
        {
            var user = HttpContext.User;
            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return Unauthorized(new { message = "Session invalid or expired." });
            }
            var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var userRole = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var userName = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            
            return Ok(new { userId = userId, userName = userName, userRole = userRole });
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserDTO>> AddUser(CreateUserDTO user)
        {
            var mapUser = _mapper.Map<User>(user);
            var newUser = await _userRepository.AddUser(mapUser);
            return Ok(newUser);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<CreateUserDTO>> DeleteItem(int id)
        {
            var deleteUser = await _userRepository.DeleteUser(id);
            if (deleteUser == null)
            {
                return NotFound();
            }
            return Ok();
        }
        [HttpGet]
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateUserDTO>> GetUserId(int id)
        {
            var user = await _userRepository.GetUserId(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPatch("{id}")]
        public async Task<ActionResult<PatchUserDTO>> UpdateUser(int id, PatchUserDTO user)
        {
            var updated = _mapper.Map<User>(user);
            updated.Id = id;
            var updatedUser = await _userRepository.UpdateUser(id, updated);
            if (updatedUser == null)
            {
                return NotFound();
            }

            return Ok(updatedUser);
        }
        
    }
}
