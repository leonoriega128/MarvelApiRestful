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

        public bool Captured { get; set; }

        public int MarvelID { get; set; }

      

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
        public int id { get; set; }
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


    public class StoryDataWrapper
    {
        public int code { get; set; }
        public string status { get; set; }
        public string copyright { get; set; }
        public string attributionText { get; set; }
        public string attributionHTML { get; set; }
        public StoryDataContainer data { get; set; }
        public string etag { get; set; }
    }

    public class StoryDataContainer
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public List<Story> results { get; set; }
    }

    public class Story
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string resourceURI { get; set; }
        public string type { get; set; }
        public string? modified { get; set; }
        public Image thumbnail { get; set; }
    }

    public class Image
    {
        public string path { get; set; }
        public string extension { get; set; }
    }

}
