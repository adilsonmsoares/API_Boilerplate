using DTO.Entities.Models.Example;
using FluentValidation;

namespace DTO.Entities.Validators.Example
{
    public class ExampleDtoValidator : AbstractValidator<ExampleDto>
    {
        public ExampleDtoValidator()
        {
            RuleFor(x => x.Property)
                .NotEmpty()
                .NotEqual("Teste");
        }
    }
}
