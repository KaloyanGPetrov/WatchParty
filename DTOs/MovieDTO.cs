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

        public List<ActorDTO> ?Actors { get; set; }

        public List<CategoryDTO> ?Categories { get; set; } 

        public string CategoryNames
        {
            get
            {
                string names;
                if (Categories.Count > 0)
                {
                    names = Categories.First().Name;
                    if (Categories.Count <=5) 
                        for (int i = 1; i < Categories.Count; i++) names += $", {Categories[i].Name}";
                    else
                    {
                        for (int i = 1; i < 5; i++) names += $", {Categories[i].Name}";
                        names += ", ...";
                    }
                }
                else names = "none";
                return names;
            }
        }
    }
}
