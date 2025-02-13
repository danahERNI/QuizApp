using System.Xml.XPath;
using AutoMapper;
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
