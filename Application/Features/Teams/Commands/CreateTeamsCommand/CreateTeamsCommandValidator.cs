using FluentValidation;

namespace Application.Features.Teams.Commands.CreateTeamsCommand
{
    public class CreateTeamsCommandValidator : AbstractValidator<CreateTeamsCommand>
    {

        public CreateTeamsCommandValidator()
        {
            RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }
    }
}
