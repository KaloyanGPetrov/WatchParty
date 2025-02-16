using WatchParty.Data.Enteties;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WatchParty.DTOs
{
    public class ActorDTO : BaseDTO
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }

        public string ImageUrl { get; set; }

        public ICollection<MovieDTO> ?Movies { get; set;}
    }
}
