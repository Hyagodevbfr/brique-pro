using BriquePro.Domain.Common.ErrorsHandling;
using Flunt.Notifications;
using Flunt.Validations;

namespace BriquePro.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Icon { get; private set; } = string.Empty;
        public string Color { get; private set; } = string.Empty;

        protected Category() { }

        private Category(string name, string icon, string color)
        {
            Name = name;
            Icon = icon;
            Color = color;

            Validate(name, icon, color);
        }

        public static Result<Category> Create(string name, string icon, string color)
        {
            var category = new Category(name, icon, color);

            if (!category.IsValid)
            {
                return Result<Category>.Failure(
                    ValidationError.FromNotifications(category.Notifications)
                );
            }

            return Result<Category>.Success(category);
        }

        public Result<Category> Update(string name, string icon, string color)
        {
            Validate(name, icon, color);

            if (!IsValid)
            {
                return Result<Category>.Failure(
                    ValidationError.FromNotifications(Notifications)
                );
            }

            Name = name;
            Icon = icon;
            Color = color;

            MarkAsUpdated();

            return Result<Category>.Success(this);
        }

        public Result<Category> ChangeColor(string color)
        {
            if (string.IsNullOrWhiteSpace(color))
            {
                AddNotification(nameof(Color), "Color cannot be empty.");

                return Result<Category>.Failure(
                    ValidationError.FromNotifications(Notifications)
                );
            }

            Color = color;
            MarkAsUpdated();

            return Result<Category>.Success(this);
        }
        private void Validate(string name, string icon, string color)
        {
            Clear();

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(name, nameof(Name), "Name is required.")
                .IsLowerOrEqualsThan(name, 60, nameof(Name), "Name must have up to 60 characters.")
                .IsNotNullOrWhiteSpace(icon, nameof(Icon), "Icon is required.")
                .IsNotNullOrWhiteSpace(color, nameof(Color), "Color is required.")
            );
        }
    }
}