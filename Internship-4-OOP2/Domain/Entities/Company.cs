using Domain.Enums;
using Domain.Exceptions;

namespace Internship_4_OOP2.Domain.Entities
{
    public class Company
    {
        public const int NameMaxLength = 150;

        public int Id { get; private set; }
        public string Name { get; private set; } = null!;

        private Company() { } 

        public Company(string name)
        {
            Validate(name);
            Name = name;
        }

        public void Update(string name)
        {
            Validate(name);
            Name = name;
        }

        private void Validate(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length > NameMaxLength)
                throw new ValidationException(
                    message: $"Company name must not be empty and must be <= {NameMaxLength} chars.",
                    code: "CMP_NAME_INVALID",
                    severity: Severity.Error
                );
        }
    }
}
