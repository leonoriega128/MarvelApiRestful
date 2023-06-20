using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TeamsDTO
    {
        public string Name { get; set; }
        public int NumberMembers { get; set; }
        public string ForceMember { get; set; }
        public string IntelliMember { get; set; }
        public string AgilityMember { get; set; }
    }
}
