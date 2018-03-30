using Infrastructure.Core;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Repositories
{
    public class GenreRepository : BaseRepository<Genre>, IGenreRepository
    {
        public GenreRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}