using Models;
using Autofac;
using Autofac.Integration.WebApi;
using Infrastructure.Core;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace WEBAPI
{
    public class AutofacWebApiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }

        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<CinemaContext>()
                   .As<DbContext>()
                   .InstancePerRequest();

            builder.RegisterType<DbFactory>()
                .As<IDbFactory>()
                .InstancePerRequest();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerRequest();

            // Repositories
            builder.RegisterGeneric(typeof(BaseRepository<>))
                   .As(typeof(IBaseRepository<>))
                   .InstancePerRequest();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>().InstancePerRequest();
            builder.RegisterType<MovieRepository>().As<IMovieRepository>().InstancePerRequest();

            // Services
            builder.RegisterType<GenreService>().As<IGenreService>().InstancePerRequest();
            builder.RegisterType<MovieService>().As<IMovieService>().InstancePerRequest();

            Container = builder.Build();

            return Container;
        }
    }
}