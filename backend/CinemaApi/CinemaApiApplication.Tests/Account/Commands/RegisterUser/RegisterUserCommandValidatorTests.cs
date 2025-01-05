using Xunit;
using CinemaApiApplication.Account.Commands.RegisterUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;

namespace CinemaApiApplication.Account.Commands.RegisterUser.Tests
{
    public class RegisterUserCommandValidatorTests
    {
        private readonly RegisterUserCommandValidator _validator;

        public RegisterUserCommandValidatorTests()
        {
            _validator = new RegisterUserCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var command = new RegisterUserCommand { Email = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email)
                  .WithErrorMessage("Email is required");
        }

        [Fact]
        public void Should_Have_Error_When_FirstName_Is_Empty()
        {
            var command = new RegisterUserCommand { FirstName = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.FirstName)
                  .WithErrorMessage("First name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_LastName_Is_Empty()
        {
            var command = new RegisterUserCommand { LastName = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.LastName)
                  .WithErrorMessage("Last name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var command = new RegisterUserCommand { Password = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Too_Short()
        {
            var command = new RegisterUserCommand { Password = "123" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password)
                  .WithErrorMessage("Password must be at least 4 characters long.");
        }

        [Fact]
        public void Should_Not_Have_Errors_For_Valid_Command()
        {
            var command = new RegisterUserCommand
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                Password = "password123"
            };

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}