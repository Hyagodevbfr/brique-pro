using BriquePro.Domain.Entities;
using BriquePro.Domain.Common.ErrorsHandling;
using FluentAssertions;
using Xunit;

namespace BriquePro.Domain.Tests.Entities
{
    public class CategoryTests
    {
        [Fact]
        public void Create_ShouldReturnSuccess_WhenValidData()
        {
            // Arrange
            var name = "Food";
            var icon = "food-icon";
            var color = "#FFF";

            // Act
            var result = Category.Create(name, icon, color);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Error.Should().BeNull();
            result.Value!.Name.Should().Be(name);
            result.Value.Icon.Should().Be(icon);
            result.Value.Color.Should().Be(color);
        }

        [Fact]
        public void Create_ShouldReturnValidationError_WhenNameIsEmpty()
        {
            // Arrange
            var icon = "x";
            var color = "y";

            // Act
            var result = Category.Create("", icon, color);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType<ValidationError>();

            var validation = (ValidationError)result.Error!;
            validation.Errors.Should().Contain(e => e.Code == "Name");
        }

        [Fact]
        public void Create_ShouldReturnValidationError_WhenNameIsTooLong()
        {
            var name = new string('a', 61);
            var result = Category.Create(name, "i", "c");

            result.IsSuccess.Should().BeFalse();
            var errors = (ValidationError)result.Error!;

            errors.Errors.Should().Contain(e => e.Code == "Name");
        }

        [Fact]
        public void Update_ShouldReturnSuccess_WhenValidData()
        {
            // Arrange
            var category = Category.Create("Old", "icon", "color").Value!;

            // Act
            var result = category.Update("New", "new-icon", "blue");

            // Assert
            result.IsSuccess.Should().BeTrue();
            category.Name.Should().Be("New");
            category.Icon.Should().Be("new-icon");
            category.Color.Should().Be("blue");
        }

        [Fact]
        public void Update_ShouldReturnValidationError_WhenInvalidData()
        {
            // Arrange
            var category = Category.Create("Valid", "icon", "color").Value!;

            // Act
            var result = category.Update("", "icon", "color");

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType<ValidationError>();

            var errors = (ValidationError)result.Error!;
            errors.Errors.Should().Contain(e => e.Code == "Name");
        }

        [Fact]
        public void ChangeColor_ShouldReturnSuccess_WhenValidColor()
        {
            // Arrange
            var category = Category.Create("Valid", "x", "red").Value!;

            // Act
            var result = category.ChangeColor("blue");

            // Assert
            result.IsSuccess.Should().BeTrue();
            category.Color.Should().Be("blue");
        }

        [Fact]
        public void ChangeColor_ShouldReturnError_WhenColorIsEmpty()
        {
            // Arrange
            var category = Category.Create("Valid", "x", "red").Value!;

            // Act
            var result = category.ChangeColor("");

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().BeOfType<ValidationError>();

            var error = (ValidationError)result.Error!;
            error.Errors.Should().Contain(e => e.Code == "Color");
        }
    }
}