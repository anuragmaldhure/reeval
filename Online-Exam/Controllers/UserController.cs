using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_Exam.Models;

namespace Online_Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<Online_ExamUser> _userManager;

        public UserController(UserManager<Online_ExamUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: api/User/GetAllUsers
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }

            return Ok(users);
        }

        // GET: api/User/GetUserById/{userId}
        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"User with ID '{userId}' not found.");
            }

            return Ok(user);
        }
    }
}
