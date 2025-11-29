using Application.DTOs.Users;
using Application.Services;
using Internship_4_OOP2.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Internship_4_OOP2.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;
        private readonly ExternalImportService _importService;

        public UsersController(UserService service, ExternalImportService importService)
        {
            _service = service;
            _importService = importService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return Created($"/api/users/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateUserDto dto)
        {
            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpPut("activate/{id}")]
        public async Task<IActionResult> Activate(int id)
        {
            await _service.ActivateAsync(id);
            return Ok();
        }

        [HttpPut("deactivate/{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _service.DeactivateAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("import-external")]
        public async Task<IActionResult> ImportExternal()
        {
            await _importService.ImportExternalUsersAsync();
            return Ok("Imported (or cache used).");
        }
    }
}
