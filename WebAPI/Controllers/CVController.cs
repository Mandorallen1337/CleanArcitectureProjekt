using Application.Interfaces.RepoInterface;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CVController : ControllerBase
    {
        private readonly IRepository<CV> _repository;

        public CVController(IRepository<CV> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cvs = await _repository.GetAllAsync();
            return Ok(cvs);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var cv = await _repository.GetByIdAsync(id, cancellationToken);
            if (cv == null)
                return NotFound("CV not found.");

            return Ok(cv);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CV cv)
        {
            var createdCV = await _repository.CreateAsync(cv);
            return CreatedAtAction(nameof(GetById), new { id = createdCV.Id }, createdCV);
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CV cv, CancellationToken cancellationToken)
        {
            if (id != cv.Id)
            return BadRequest("CV ID mismatch.");

            await _repository.UpdateAsync(cv, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _repository.DeleteByIdAsync(id);
            if (result == "Entity not found")
                return NotFound(result);

            return Ok(result);
        }
    }
}
