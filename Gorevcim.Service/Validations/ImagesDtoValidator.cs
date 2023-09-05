using FluentValidation;
using Gorevcim.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorevcim.Service.Validations
{
    public class ImagesDtoValidator: AbstractValidator<ImagesDto>
    {
        public ImagesDtoValidator()
        {
            RuleFor(x => x.LogoBase64).NotEmpty().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
           
        }
    }
}
