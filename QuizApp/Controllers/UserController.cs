using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.XPath;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Data.DTO;
using QuizApp.Entities;
using QuizApp.Repositories.Interfaces;

namespace QuizApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userRepository.ValidateUser(loginDTO.Email, loginDTO.Password);

            if (user != null) 
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserId", user.Id.ToString());

                return Ok(new { Id = user.Id, name = user.Name, role = user.Role});

            }

            return Unauthorized();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    
            HttpContext.Session.Clear();

            return Ok();
        }

        [HttpGet("session-data")]
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

            //var userRole = HttpContext.Session.GetString("UserRole");

            //var userName = HttpContext.Session.GetString("UserName");

            //var userId = HttpContext.Session.GetString("UserId");
            
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
