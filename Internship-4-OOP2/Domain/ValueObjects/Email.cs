using System.Text.RegularExpressions;
using Domain.Exceptions;
using Domain.Enums;

namespace Internship_4_OOP2.Domain.ValueObjects
{
    public class Email
    {
        public string? Value { get; }

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value) ||
                !Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ValidationException(
                    message: "Invalid email format.",
                    code: "USR_EMAIL_INVALID",
                    severity: Severity.Error
                );
            }

            Value = value;
        }
        public override string ToString() => Value;
    }
}
