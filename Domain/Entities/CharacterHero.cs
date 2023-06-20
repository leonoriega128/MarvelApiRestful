using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CharacterHero : AuditableBaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? UrlImage { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int Force { get; set; }

        public bool Capturado { get; set; }

      

    }

    public class MarvelCharacterDataWrapper
    {
        public MarvelCharacterDataContainer Data { get; set; }
    }

    public class MarvelCharacterDataContainer
    {
        public MarvelCharacter[] Results { get; set; }
    }

    public class MarvelCharacter
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public MarvelUrlImage? thumbnail { get; set; }
        public string? modified { get; set; }
        public MarvelSeriesList Series { get; set; }
    }

    public class MarvelSeriesList
    {
        public MarvelSeries[] Items { get; set; }
    }

    public class MarvelSeries
    {
        public string Name { get; set; }
    }

    public class MarvelUrlImage
    {
        public string path { get; set; }
        public string extension { get; set; }
    }
 
}
