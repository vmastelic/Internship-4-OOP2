using Application.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Internship_4_OOP2.Application.DTOs.External;
using System.Net.Http.Json;
using System.Text.Json;

namespace Internship_4_OOP2.Infrastructure.External
{
    public class ExternalUserService : IExternalUserService
    {
        private readonly HttpClient _http;

        public ExternalUserService(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        }

        public async Task<IEnumerable<ExternalUserDto>> GetExternalUsersAsync()
        {
            try
            {
                var users = await _http.GetFromJsonAsync<List<JsonElement>>("users");

                if (users == null)
                    throw new ValidationException("External data empty.", "EXT_API_EMPTY", Severity.Error);

                return users.Select(u => new ExternalUserDto
                {
                    Id = u.GetProperty("id").GetInt32(),
                    Name = u.GetProperty("name").GetString()!,
                    Username = u.GetProperty("username").GetString()!,
                    Email = u.GetProperty("email").GetString()!,

                    Street = u.GetProperty("address").GetProperty("street").GetString()!,
                    City = u.GetProperty("address").GetProperty("city").GetString()!,

                    Lat = double.Parse(u.GetProperty("address").GetProperty("geo").GetProperty("lat").GetString()!),
                    Lng = double.Parse(u.GetProperty("address").GetProperty("geo").GetProperty("lng").GetString()!),

                    Website = u.GetProperty("website").GetString(),
                    CompanyName = u.GetProperty("company").GetProperty("name").GetString()!,
                });

            }
            catch (HttpRequestException)
            {
                throw new ValidationException(
                    "External API is not available.",
                    "EXT_API_UNAVAILABLE",
                    Severity.Error);
            }
        }
    }
}
