namespace WatchParty.Data.Enteties
{
    public class MovieCategory
    {
        public int MovieID { get; set; }
        public int CategoryID { get; set; }
        public virtual Movie Movie { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
    }
}
