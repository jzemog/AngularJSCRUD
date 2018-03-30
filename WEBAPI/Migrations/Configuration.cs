namespace WEBAPI.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CinemaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CinemaContext context)
        {
            GetGenres().ForEach(g => context.Genre.Add(g));
        }

        private static List<Genre> GetGenres()
        {
            var genre = new List<Genre> {
                new Genre
                {
                    ID = 1,
                    Name = "Comedy"
                },
                new Genre
                {
                    ID = 2,
                    Name = "Sci-fi"
                },
                new Genre
                {
                    ID = 3,
                    Name = "Action"
                },
                new Genre
                {
                    ID = 4,
                    Name = "Horror"
                },
                new Genre
                {
                    ID = 5,
                    Name = "Romance"
                },
                new Genre
                {
                    ID = 6,
                    Name = "Crime"
                }
            };

            return genre;
        }

    }
}
