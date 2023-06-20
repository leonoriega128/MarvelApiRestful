using FluentValidation;

namespace Application.Features.CharactersHero.Commands.UpdateCharacterCommand
{
    public class UpdateCharacterCommandValidator : AbstractValidator<UpdateCharacterCommand>
    {
        public UpdateCharacterCommandValidator()
        {
            RuleFor(p => p.Id)
             .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
            RuleFor(p => p.Name)
           .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }
    }
}
