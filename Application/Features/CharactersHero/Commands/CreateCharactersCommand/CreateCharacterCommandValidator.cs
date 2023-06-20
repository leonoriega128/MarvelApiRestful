using FluentValidation;

namespace Application.Features.CharactersHero.Commands.CreateCharactersCommand
{
    public class CreateCharacterCommandValidator : AbstractValidator<CreateCharacterCommand>
    {
        public CreateCharacterCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
            //RuleFor(x => x.Description)
            //    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");

        }
    }
}
