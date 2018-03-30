using Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModels;

namespace Infrastructure.Mapping
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            try
            {
                Mapper.Initialize((config) =>
                {
                    config.CreateMap<Genre, GenreViewModel>()
                        .ReverseMap();
                    config.CreateMap<Movie, MovieViewModel>()
                        .ForMember(vm => vm.Genre, map => map.MapFrom(m => m.Genre.Name))
                        .ReverseMap()
                            .ForMember(m => m.Genre, map => map.Ignore());
                });

                Mapper.AssertConfigurationIsValid();
            }
            catch (Exception)
            {
                //https://www.youtube.com/watch?v=fXvPmxrrnQY
            }
        }
    }
}