using Flunt.Notifications;

namespace BriquePro.Domain.Common.ErrorsHandling
{
    public sealed record ValidationError(IReadOnlyCollection<Error> Errors)
    {
        public static ValidationError FromNotifications(
        IReadOnlyCollection<Notification> notifications)
        {
            var errors = notifications
                .Select(n => new Error(n.Key, n.Message))
                .ToList();

            return new ValidationError(errors);
        }
    }
}