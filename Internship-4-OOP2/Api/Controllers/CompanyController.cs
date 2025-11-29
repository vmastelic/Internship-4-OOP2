using Application.DTOs.Companies;
using Internship_4_OOP2.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Internship_4_OOP2.Api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly CompanyService _service;

        public CompaniesController(CompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string username, string password)
            => Ok(await _service.GetAllAsync(username, password));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, string username, string password)
        {
            var result = await _service.GetByIdAsync(id, username, password);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCompanyDto dto, string username, string password)
        {
            var id = await _service.CreateAsync(dto, username, password);
            return Created($"/api/companies/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCompanyDto dto, string username, string password)
        {
            await _service.UpdateAsync(id, dto, username, password);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, string username, string password)
        {
            await _service.DeleteAsync(id, username, password);
            return NoContent();
        }
    }
}
