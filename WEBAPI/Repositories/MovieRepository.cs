using Infrastructure.Core;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}