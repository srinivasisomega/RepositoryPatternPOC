using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace EmployeeRegistration.Controllers
{
  
        [ApiController]
        [Route("api/[controller]")]
        public class EmployeeController : ControllerBase
        {
            private readonly IEmployeeService _service;

            public EmployeeController(IEmployeeService service)
            {
                _service = service;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                return Ok(await _service.GetAllAsync());
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> Get(int id)
            {
                var employee = await _service.GetByIdAsync(id);
                return employee == null ? NotFound() : Ok(employee);
            }

            [HttpPost]
            public async Task<IActionResult> Create(EmployeeCreateDto dto)
            {
                var id = await _service.CreateAsync(dto);
                return CreatedAtAction(nameof(Get), new { id }, null);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, EmployeeUpdateDto dto)
            {
                 _service.UpdateAsync(id, dto);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                 _service.DeleteAsync(id);
                return NoContent();
            }
        [HttpPost("paged")]
        public async Task<IActionResult> GetEmployeesPaged(
    [FromBody] EmployeePageRequestDto request)
        {
            var result = await _service.GetPagedAsync(
                request.PageNumber,
                request.PageSize);

            return Ok(result);
        }

    }


}
