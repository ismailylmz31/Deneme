using Microsoft.AspNetCore.Mvc;
using TechCareer.Models.Dtos.Instructors;
using TechCareer.Models.Entities;
using TechCareer.Service.Abstracts;

namespace TechCareer.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        // Tüm Instructorları Listeleme
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var instructors = await _instructorService.GetListAsync();
            return Ok(instructors);
        }

        // Belirli Bir Instructor Detayı
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var instructor = await _instructorService.GetByIdAsync(id);
                return Ok(instructor);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Instructor not found.");
            }
        }

        // Yeni Instructor Ekleme
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateInstructorRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var instructor = await _instructorService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = instructor.Id }, instructor);
        }

        // Instructor Güncelleme
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInstructorRequestDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var instructor = await _instructorService.UpdateAsync(id, dto);
            return Ok("Instructor updated successfully.");
        }

        // Instructor Silme
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _instructorService.DeleteAsync(id);
                return Ok("Instructor deleted successfully.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Instructor not found.");
            }
        }
    }
}