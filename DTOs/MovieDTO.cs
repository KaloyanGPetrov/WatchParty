using WatchParty.Data.Enteties;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WatchParty.DTOs
{
    public class MovieDTO : BaseDTO
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string VideoPath { get; set; }

        public ICollection<ActorDTO> ?Actors { get; } = [];

        public ICollection<CategoryDTO> ?Categories { get; } = [];
    }
}
