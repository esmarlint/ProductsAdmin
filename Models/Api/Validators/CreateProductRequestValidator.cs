using FluentValidation;
using ProductsAdmin.Models.Api.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAdmin.Models.Api.Validators
{
    public class CreateProductRequestValidator:AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("El nombre está vacío");
            RuleFor(e => e.Description).NotEmpty().WithMessage("La descripción está vacía");
            //RuleFor(e => e.Prices).NotNull().SetValidator(new UpdateProductRequestValidator());
        }
    }


    public class CreatePriceRequestValidator : AbstractValidator<CreatePriceRequest>
    {
        public CreatePriceRequestValidator()
        {
            //RuleFor(e => e.Name).NotEmpty().WithMessage("El nombre está vacío");
            //RuleFor(e => e.Description).NotEmpty().WithMessage("La descripción está vacía");
        }
    }

    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("El nombre está vacío");
            RuleFor(e => e.Description).NotEmpty().WithMessage("La descripción está vacía");
        }
    }

    
}
