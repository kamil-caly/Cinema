﻿using FluentValidation;

namespace CinemaApiApplication.Account.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(4).WithMessage("Password must be at least 4 characters long.");
        }
    }
}
