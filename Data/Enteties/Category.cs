namespace WatchParty.Data.Enteties
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<MovieCategory> MovieCategories { get; set;}

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
