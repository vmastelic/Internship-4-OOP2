using Domain.Enums;
using Domain.Exceptions;

namespace Internship_4_OOP2.Domain.ValueObjects
{
    public class Website
    {
        public string? Value { get; }

        public Website(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                Value = null;
                return;
            }

            if (!IsValidUrl(value))
            {
                throw new ValidationException(
                    message: "Invalid website URL format.",
                    code: "USR_WEBSITE_INVALID",
                    severity: Severity.Warning
                );
            }

            Value = value;
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var result)
                && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }

        public override string ToString() => Value ?? string.Empty;
    }
}
