using Application.DTOs.Users;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Internship_4_OOP2.Domain.ValueObjects;

namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly GeoLocation _referenceLocation;
        public UserService(IUserRepository userRepository, double refLat, double refLng)
        {
            _userRepository = userRepository;
            _referenceLocation = new GeoLocation(refLat, refLng);
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Name = u.Name,
                Username = u.Username,
                Email = u.Email.Value,
                AdressCity = u.AddressCity,
                AdressStreet = u.AddressStreet,
                GeoLat = u.Location.Lat,
                GeoLng = u.Location.Lng,
                Website = u.Website?.Value,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            });
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email.Value,
                AdressStreet = user.AddressStreet,
                AdressCity = user.AddressCity,
                GeoLat = user.Location.Lat,
                GeoLng = user.Location.Lng,
                Website = user.Website?.Value,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt

            };
        }

        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            if (await _userRepository.ExistsByEmailAsync(dto.Email))
                throw new ValidationException("Email already exists.", "USER_EMAIL_EXISTS", Severity.Error);

            if (await _userRepository.ExistsByUsernameAsync(dto.Username))
                throw new ValidationException("Username already exists.", "USR_USERNAME_EXISTS", Severity.Error);

            var email = new Email(dto.Email);
            var website = new Website(dto.Website);
            var location = new GeoLocation(dto.GeoLat, dto.GeoLng);

            var distance = location.DistanceTo(_referenceLocation);
            if (distance > 3)
                throw new ValidationException(
                    "User must be within 3km from reference location.",
                    "USR_LOCATION_OUT_OF_RANGE",
                    Severity.Error);

            var password = Guid.NewGuid().ToString();

            var user = new User(
                dto.Name,
                dto.Username,
                email,
                dto.AddressStreet,
                dto.AddressCity,
                location,
                website,
                password);

            await _userRepository.AddAsync(user);

            return user.Id;

        }

        public async Task UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new ValidationException("User not found.", "USR_NOT_FOUND", Severity.Error);

            var website = new Website(dto.Website);
            var location = new GeoLocation(dto.GeoLat, dto.GeoLng);

            var distance = location.DistanceTo(_referenceLocation);
            if (distance > 3)
                throw new ValidationException(
                    "Must be within 3km.",
                    "USR_LOCATION_OUT_OF_RANGE",
                    Severity.Error);

            user.Update(dto.Name, dto.AddressStreet, dto.AddressCity, website);

            await _userRepository.UpdateAsync(user);
        }

        public async Task ActivateAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new ValidationException("User not found.", "USR_NOT_FOUND", Severity.Error);

            user.Activate();
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeactivateAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new ValidationException("User not found.", "USR_NOT_FOUND", Severity.Error);

            user.Deactivate();
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
