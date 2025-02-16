using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace WatchParty.Data.Enteties
{
    public class Movie : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string VideoPath { get; set; }

        public virtual ICollection<MovieActor> MovieActors {  get; set; }
        public virtual ICollection<Actor> Actors { get; set; }
        
        public virtual ICollection<MovieCategory> MovieCategories {  get; set; }
        public virtual ICollection<Category> Categories { get; set; }

    }
}
