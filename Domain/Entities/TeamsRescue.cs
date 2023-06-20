using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TeamsRescue : AuditableBaseEntity
    {
        public string? Name { get; set; }
    }
    public class ListTeams
    {
        public HeroAtribbs[] Items { get; set; }
        public int NumberMembers { get; set; }
    }

    public class HeroAtribbs
    {
        public string Name { get; set; }
        public int PrimaryAtribb { get; set; }
    }
}
