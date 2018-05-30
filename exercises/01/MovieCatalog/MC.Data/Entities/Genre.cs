using System.Collections.Generic;

namespace MC.Data.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
