using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CharacterHeroDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? UrlImage { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? TeamName { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int Force { get; set; }

        public bool Captured { get; set; }
        public int MarvelID { get; set; }
    }
}
