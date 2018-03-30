using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Models;
using Services;
using ViewModels;

namespace WEBAPI.Controllers
{
    [RoutePrefix("api/genres")]
    public class GenresController : ApiController
    {
        private readonly IGenreService genreService;

        public GenresController(IGenreService genreService)
        {
            this.genreService = genreService;
        }

        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;

            var genres = genreService.GetGenres();

            var genresVm = Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModel>>(genres);

            response = request.CreateResponse<IEnumerable<GenreViewModel>>(HttpStatusCode.OK, genresVm);

            return response;
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, GenreViewModel genreVM)
        {
            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                Genre newGenre = Mapper.Map<Genre>(genreVM);
                genreService.AddGenre(newGenre);
                genreVM = Mapper.Map<Genre, GenreViewModel>(newGenre);
                response = request.CreateResponse<GenreViewModel>(HttpStatusCode.Created, genreVM);
            }
            return response;
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, GenreViewModel genreVM)
        {
            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                Genre _genre = Mapper.Map<Genre>(genreVM);
                genreService.UpdateGenre(_genre);
                response = request.CreateResponse(HttpStatusCode.OK);
            }

            return response;
        }

        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;

            var genre = genreService.GetGenre(id);

            GenreViewModel genreVM = Mapper.Map<Genre, GenreViewModel>(genre);

            response = request.CreateResponse<GenreViewModel>(HttpStatusCode.OK, genreVM);

            return response;
        }

        [HttpDelete]
        [Route("remove/{id:int}")]
        public HttpResponseMessage Remove(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;

            var genre = genreService.GetGenre(id);

            if (genre == null)
            {
                response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                genreService.DeleteGenre(genre);
                response = request.CreateResponse(HttpStatusCode.OK);
            }

            return response;
        }

    }
}