using Domain.Enums;

namespace Domain.Exceptions
{
    public class ValidationException : Exception
    {
        public string Code { get; }
        public Severity Severity { get; }

        public ValidationException(
            string message,
            string code,
            Severity severity = Severity.Error)
            : base(message)
        {
            Code = code;
            Severity = severity;
        }
    }
}
