using FluentValidation;

namespace Application.Features.Teams.Commands.UpdateTeamsCommand
{
    public class UpdateTeamsCommandValidator : AbstractValidator<UpdateTeamsCommand>
    {
        public UpdateTeamsCommandValidator()
        {
            RuleFor(p => p.Id)
             .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
            RuleFor(p => p.Name)
           .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }
    }
}
