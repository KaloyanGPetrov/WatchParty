using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WatchParty.Data.Enteties
{
    public class Actor : BaseEntity
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Age { get; set; }


        public string ImageUrl { get; set; }

        public virtual ICollection<MovieActor> MovieActors {get; set; }

        public virtual ICollection<Movie> Movies { get; set;}
    }

}
