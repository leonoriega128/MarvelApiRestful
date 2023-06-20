using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Teams_Hero : AuditableBaseEntity
    {
        public int IdHero { get; set; }
        public int IdTeam { get; set; }
    }

    public class CreateNameRequest
    {
        public string teamname { get; set; }
        public List<ListHero> HeroList { get; set; }
    }
    public class ListHero
    {
        public List<int> idHero { get; set; }
    }
}
