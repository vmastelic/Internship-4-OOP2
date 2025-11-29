using Domain.Enums;
using Domain.Exceptions;
using Internship_4_OOP2.Domain.ValueObjects;

namespace Domain.Entities
{
    public class User
    {

        public const int NameMaxLength = 100;
        public const int UsernameMaxLength = 100;
        public const int AddressStreetMaxLength = 150;
        public const int AddressCityMaxLength = 100;

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public Email Email { get; set; } = null!;
        public string AddressStreet { get; set; } = null!;
        public string AddressCity { get; set; } = null!;
        public GeoLocation Location { get; set; } = null!;
        public Website? Website { get; set; }
        public string Password { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        private User() { }

        public User(
            string name,
            string username,
            Email email,
            string addressStreet,
            string addressCity,
            GeoLocation location,
            Website? website,
            string password)
        {
            Validate(name, username, addressStreet, addressCity);

            Name = name;
            Username = username;
            Email = email;
            AddressStreet = addressStreet;
            AddressCity = addressCity;
            Location = location;
            Website = website;
            Password = password;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public User(
            int id,
            string name,
            string username,
            Email email,
            string addressStreet,
            string addressCity,
            GeoLocation location,
            Website? website,
            string password,
            DateTime createdAt,
            DateTime updatedAt,
            bool isActive)
        {
            Id = id;
            Name = name;
            Username = username;
            Email = email;
            AddressStreet = addressStreet;
            AddressCity = addressCity;
            Location = location;
            Website = website;
            Password = password;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            IsActive = isActive;
        }

        private void Validate(string name, string username, string street, string city)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > NameMaxLength)
                throw new ValidationException(
                    "Name too long.",
                    code: "USR_NAME_INVALID",
                    severity: Severity.Error);

            if (string.IsNullOrWhiteSpace(username) || username.Length > UsernameMaxLength)
                throw new ValidationException(
                    "Username too long.",
                    code: "USR_USERNAME_INVALID",
                    severity: Severity.Warning);

            if (string.IsNullOrWhiteSpace(street) || street.Length > AddressStreetMaxLength)
                throw new ValidationException(
                    "Street name too long.",
                    code: "USR_STREET_INVALID",
                    severity: Severity.Warning);

            if (string.IsNullOrWhiteSpace(city) || city.Length > AddressCityMaxLength)
                throw new ValidationException(
                    "City name too long.",
                    code: "USR_CITY_INVALID",
                    severity: Severity.Warning);
        }

        public void Update(string name, string street, string city, Website? website)
        {
            Validate(name, Username, street, city);

            Name = name;
            AddressStreet = street;
            AddressCity = city;
            Website = website;

            UpdatedAt = DateTime.UtcNow;
        }

        public void Deactivate() => IsActive = false;
        public void Activate() => IsActive = true;
    }
}