using FluentValidation;

namespace Application.Features.TeamsHero.Commands.DeleteTeamsHeroCommand
{
    public class DeleteTeamsHeroCommandValidator : AbstractValidator<DeleteTeamsHeroCommand>
    {
        public DeleteTeamsHeroCommandValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }
    }
}
