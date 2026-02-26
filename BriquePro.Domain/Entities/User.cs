using BriquePro.Domain.ValueObjects;

namespace BriquePro.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public EmailAddress Email { get; private set; }
        public string PasswordHash { get; private set; } = string.Empty;
        public bool IsPremium { get; private set; }
        public DateTime PremiumUntil { get; private set; }

        protected User() { }



    }
}
