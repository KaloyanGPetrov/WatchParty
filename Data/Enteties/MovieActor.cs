using Azure;
using Microsoft.Extensions.Hosting;

namespace WatchParty.Data.Enteties
{
    public class MovieActor
    {
        public int MovieID { get; set; }
        public int ActorID { get; set; }
        public virtual Movie Movie { get; set; } = null!;
        public virtual Actor Actor { get; set; } = null!;
    }
}
