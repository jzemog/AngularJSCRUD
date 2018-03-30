using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Core
{
    public class DbFactory : Disposable, IDbFactory
    {
        CinemaContext dbContext;

        public CinemaContext Init()
        {
            return dbContext ?? (dbContext = new CinemaContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
