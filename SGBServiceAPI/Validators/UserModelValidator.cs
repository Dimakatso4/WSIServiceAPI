using FluentValidation;
using WSIServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSIServiceAPI.Validators
{
    public class CreateUserModelValidator : AbstractValidator<UserModel>
    {
        ///<Summary>
        /// User validations constructor
        ///</Summary>
        public CreateUserModelValidator()
        {
            RuleFor(x => x.Firstname)
                            .NotNull()
                            .WithMessage("Firstname must be provided");
            RuleFor(x => x.Surname)
                           .NotNull()
                           .WithMessage("Surname must be provided");
         
        }

    }
}
