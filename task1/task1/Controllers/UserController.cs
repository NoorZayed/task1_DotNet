using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace task1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private static readonly List<User> users = new List<User>()
        {
            new User(1,"noor","zayed",DateOfBirth=new DateOnly(2003,3,23),"noorzayed@gmail.com"),
            new User(2,"jana","zayed",DateOfBirth=new DateOnly(2017,4,23),"janazayed@gmail.com"),
            new User(3,"hala","zayed",DateOfBirth=new DateOnly(2008,9,17),"halazayed@gmail.com")

        };
        private static DateOnly DateOfBirth;

        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(users);
        }



        [HttpGet("find-user-by-id")]
        public IActionResult FindById([FromQuery] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("ID is required");
            }

            var user = users.Find(t => t.id == id.Value);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found");
            }

            return Ok(user);
        }

        [HttpPost("add")]
        public IActionResult AddTodo([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid data.");
            }

            users.Add(newUser);
            return Ok(newUser);
        }


        [HttpPut("update-user")]
        public IActionResult UpdateUser([FromQuery] int id, [FromBody] User updatedUser)
        {
            if (id <= 0)
            {
                return BadRequest("A valid user ID is required.");
            }

            var existingUser = users.Find(t => t.id == id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            if (updatedUser == null)
            {
                return BadRequest("Updated user data is required.");
            }

            // Create a copy of the existing user data
            var originalUser = new User
            {
                id = existingUser.id,
                fname = existingUser.fname,
                lname = existingUser.lname,
                DateOfBirth = existingUser.DateOfBirth,
                email = existingUser.email
            };

            // Update the user data
            existingUser.fname = updatedUser.fname;
            existingUser.lname = updatedUser.lname;
            existingUser.DateOfBirth = updatedUser.DateOfBirth;
            existingUser.email = updatedUser.email;

            // Create a result object with both original and updated user data
            var result = new
            {
                OriginalUser = originalUser,
                UpdatedUser = existingUser
            };

            return Ok(result);
        }


        [HttpDelete("delete user by id")]
        public IActionResult Delete(int id)
        {
            var user = users.Find(t => t.id == id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            users.Remove(user);
            return Ok($"User with ID {id} has been deleted.");
        }
    }

}
