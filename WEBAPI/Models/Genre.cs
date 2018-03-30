using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Genre : IEntityBase
    {
        public Genre()
        {
            this.Movies = new HashSet<Movie>();
        }

        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}