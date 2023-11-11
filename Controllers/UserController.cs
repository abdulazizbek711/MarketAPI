using AutoMapper;
using MarketApi.Data;
using MarketApi.Dtos;
using MarketApi.Interfaces;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace MarketApi.Controllers
{
[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IUserMap _userMap;

        public UserController(IUserRepository userRepository, IMapper mapper, DataContext context, IUserService userService, IUserMap userMap)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _context = context;
            _userService = userService;
            _userMap = userMap;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }
        [HttpPost("{User_ID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser(UserDto userCreate)
        {
            var userMap = _userMap.MapUser(userCreate);
            (bool success, string message) result = _userService.CreateUser(userMap, userCreate);

            if (!result.success)
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(userCreate);
        }
        [HttpPut("{User_ID}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateUser(int User_ID, [FromBody] UserDto updatedUser)
        {
            if (updatedUser == null || User_ID != updatedUser.User_ID || !ModelState.IsValid)
                return BadRequest(ModelState);
            var existingUser = _userRepository.GetUser(User_ID);
            if (existingUser == null)
                return NotFound();
            _context.Entry(existingUser).State = EntityState.Detached;
            existingUser.UserName = updatedUser.UserName ?? existingUser.UserName;
            existingUser.Email = updatedUser.Email ?? existingUser.Email;
            existingUser.PhoneNumber = updatedUser.PhoneNumber ?? existingUser.PhoneNumber;
            var userMap = _mapper.Map<User>(existingUser);
            if (!_userService.UpdateUser(userMap))
            {
                ModelState.AddModelError("", "Something went wrong updating user");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{User_ID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteUser(int User_ID)
        {
            if (!_userRepository.UserExists(User_ID))
                return NotFound();
            var userToDelete = _userRepository.GetUser(User_ID);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_userService.DeleteUser(userToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting user");
            }
            return NoContent();
        }
    }
}