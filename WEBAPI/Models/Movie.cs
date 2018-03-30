using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Movie : IEntityBase
    {
        public int ID { get; set; }
        public int GenreId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }        

        public virtual Genre Genre { get; set; }
    }
}