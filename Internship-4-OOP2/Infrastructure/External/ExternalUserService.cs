using Application.Interfaces;
using Domain.Enums;
using Domain.Exceptions;
using Internship_4_OOP2.Application.DTOs.External;
using System.Net.Http.Json;

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
                var users = await _http.GetFromJsonAsync<List<dynamic>>("users");

                if (users == null)
                    throw new ValidationException(
                        "External data empty or malformed.",
                        "EXT_API_EMPTY",
                        Severity.Error);

                return users.Select(u => new ExternalUserDto
                {
                    Id = (int)u.id,
                    Name = (string)u.name,
                    Username = (string)u.username,
                    Email = (string)u.email,
                    Street = (string)u.address.street,
                    City = (string)u.address.city,
                    Lat = decimal.Parse((string)u.address.geo.lat),
                    Lng = decimal.Parse((string)u.address.geo.lng),
                    Website = (string?)u.website,
                    CompanyName = (string)u.company.name
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
