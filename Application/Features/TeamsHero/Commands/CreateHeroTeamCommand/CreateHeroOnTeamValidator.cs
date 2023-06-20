using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TeamsHero.Commands.CreateHeroTeamCommand
{
    public class CreateHeroOnTeamValidator : AbstractValidator<CreateHeroOnTeamCommand>
    {

        public CreateHeroOnTeamValidator()
        {
            RuleFor(x => x.IdTeam)
                    .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
            RuleFor(x => x.IdHero)
                  .NotEmpty().WithMessage("{PropertyName} no puede ser vacio.");
        }
    }
}
