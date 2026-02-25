using BriquePro.Domain.Common.ErrorsHandling;
using Flunt.Notifications;

namespace BriquePro.Domain.Entities
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        public long Id { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }

        protected BaseEntity()
        {
            CreatedAt = DateTime.UtcNow;
            IsDeleted = false;
        }
        protected void MarkAsUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        protected Result MarkAsDeleted()
        {
            if (IsDeleted)
                return Result.Failure(new Error("Entity.Deleted", "Entity is already deleted."));

            IsDeleted = true;
            MarkAsUpdated();

            return Result.Success();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not BaseEntity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode() => Id.GetHashCode();

    }
}
