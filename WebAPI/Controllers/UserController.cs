//using Application.Interfaces.RepoInterface;
//using Domain.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private readonly IRepository<User> _userRepository;

//        public UserController(IRepository<User> userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var users = await _userRepository.GetAllAsync();
//            return Ok(users);
//        }

//        [HttpGet("{id:guid}")]
//        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
//        {
//            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
//            if (user == null)
//            {
//                return NotFound("User not found.");
//            }

//            return Ok(user);
//        }

//        [HttpPost]
//        public async Task<IActionResult> Create([FromBody] User user)
//        {
//            var createdUser = await _userRepository.CreateAsync(user);
//            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
//        }


//        [HttpPut("{id:guid}")]
//        public async Task<IActionResult> Update(Guid id, [FromBody] User user, CancellationToken cancellationToken)
//        {
//            if (id != user.Id)
//            {
//                return BadRequest("User ID mismatch.");
//            }

//            await _userRepository.UpdateAsync(user, cancellationToken);
//            return NoContent();
//        }


//        [HttpDelete("{id:guid}")]
//        public async Task<IActionResult> Delete(Guid id)
//        {
//            var result = await _userRepository.DeleteByIdAsync(id);
//            if (result == "Entity not found")
//            {
//                return NotFound(result);
//            }

//            return Ok(result);
//        }
//    }
//}
