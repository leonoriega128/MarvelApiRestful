using FluentValidation;

namespace Application.Features.Teams.Commands.DeleteTeamsCommand
{
    public class DeleteTeamsCommandValidator : AbstractValidator<DeleteTeamsCommand>
    {
        public DeleteTeamsCommandValidator()
        {
            RuleFor(p => p.Id)
               .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }
    }
}
