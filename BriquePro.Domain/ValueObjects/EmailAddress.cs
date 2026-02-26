using BriquePro.Domain.Common;
using BriquePro.Domain.Common.ErrorsHandling;
using System.ComponentModel.DataAnnotations;

namespace BriquePro.Domain.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        public readonly string Value;

        private EmailAddress(string value)
        {
            Value = value;
        }

        public Result<EmailAddress> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result<EmailAddress>.Failure(new Error("Invalid.Email", "Email cannot be null or whitespace."));

            email = email.Trim();

            if (!IsValid(email))
                return Result<EmailAddress>.Failure(new Error("Invalid.Email", "Email format is invalid."));

            return new EmailAddress(email);
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        private static bool IsValid(string email) => !string.IsNullOrEmpty(email) && new EmailAddressAttribute().IsValid(email);

    }
}
