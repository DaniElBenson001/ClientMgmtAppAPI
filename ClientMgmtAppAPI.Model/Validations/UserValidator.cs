using ClientMgmtAppAPI.Common.Constants;
using ClientMgmtAppAPI.Models.DtoModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMgmtAppAPI.Models.Validations
{
    public class UserValidator : AbstractValidator<CreateUserDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage(Messages.ValidatorMessage.Required)
                .MinimumLength(ValidationConstants.MinTextLength)
                .WithMessage(Messages.ValidatorMessage.MinLengthError("FirstName", ValidationConstants.MinTextLength));

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage(Messages.ValidatorMessage.Required)
                .MinimumLength(ValidationConstants.MinTextLength)
                .WithMessage(Messages.ValidatorMessage.MinLengthError("FirstName", ValidationConstants.MinTextLength));

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage(Messages.ValidatorMessage.Required)
                .EmailAddress().WithMessage(Messages.ValidatorMessage.EmailAddressError);

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage(Messages.ValidatorMessage.Required)
                .MinimumLength(ValidationConstants.MinPasswordLength)
                .WithMessage(Messages.ValidatorMessage.MinLengthError("Password", ValidationConstants.MinPasswordLength));

            RuleFor(user => user.PhoneNumber)
                .NotEmpty().WithMessage(Messages.ValidatorMessage.Required)
                .Length(ValidationConstants.PhoneNumberLength)
                .WithMessage(Messages.ValidatorMessage.LengthError("Phone Number", ValidationConstants.PhoneNumberLength));

            RuleFor(user => user.CompanyName)
                .MinimumLength(ValidationConstants.MinTextLength)
                .WithMessage(Messages.ValidatorMessage.MinLengthError("Company Name", ValidationConstants.MinTextLength));

            RuleFor(user => user.CompanyAddress)
                .MinimumLength(ValidationConstants.MinTextLength)
                .WithMessage(Messages.ValidatorMessage.MinLengthError("Company Address", ValidationConstants.MinTextLength));
        }
    }
}
