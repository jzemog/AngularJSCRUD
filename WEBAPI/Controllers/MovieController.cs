using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Infrastructure.Core;
using Models;
using Services;
using ViewModels;

namespace WEBAPI.Controllers
{
    [RoutePrefix("api/movies")]
    public class MovieController : ApiController
    {
        private readonly IMovieService movieService;

        public MovieController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {            
            HttpResponseMessage response = null;

            var movies = movieService.GetMovies();

            var moviesVm = Mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(movies);

            response = request.CreateResponse<IEnumerable<MovieViewModel>>(HttpStatusCode.OK, moviesVm);

            return response;
        }

        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(HttpRequestMessage request, MovieViewModel movieVM)
        {
            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                //Movie newMovie = Mapper.Map<Movie>(movieVM);
                Movie newMovie = Mapper.Map<MovieViewModel, Movie>(movieVM);
                movieService.AddMovie(newMovie);
                movieVM = Mapper.Map<Movie, MovieViewModel>(newMovie);
                response = request.CreateResponse<MovieViewModel>(HttpStatusCode.Created, movieVM);
            }
            return response;
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, MovieViewModel movieVM)
        {            
            HttpResponseMessage response = null;

            if (!ModelState.IsValid)
            {
                response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                Movie _movie = Mapper.Map<Movie>(movieVM);
                movieService.UpdateMovie(_movie);
                response = request.CreateResponse(HttpStatusCode.OK);
            }

            return response;            
        }

        [Route("details/{id:int}")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;

            var movie = movieService.GetMovie(id);

            MovieViewModel movieVM = Mapper.Map<Movie, MovieViewModel>(movie);

            response = request.CreateResponse<MovieViewModel>(HttpStatusCode.OK, movieVM);

            return response;
        }

        [HttpDelete]
        [Route("remove/{id:int}")]
        public HttpResponseMessage Remove(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;

            var movie = movieService.GetMovie(id);

            if (movie == null)
            {
                response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                movieService.DeleteMovie(movie);
                response = request.CreateResponse(HttpStatusCode.OK);
            }

            return response;            
        }

        [MimeMultipart]
        [Route("images/upload")]
        public HttpResponseMessage Post(HttpRequestMessage request, int movieId)
        {
            HttpResponseMessage response = null;

            var movie = movieService.GetMovie(movieId);
            if (movie == null)
                response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid movie.");
            else
            {
                var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images");

                var multipartFormDataStreamProvider = new UploadMultipartFormProvider(uploadPath);

                // Read the MIME multipart asynchronously 
                Request.Content.ReadAsMultipartAsync(multipartFormDataStreamProvider);

                string _localFileName = multipartFormDataStreamProvider
                    .FileData.Select(multiPartData => multiPartData.LocalFileName).FirstOrDefault();

                // Create response
                FileUploadResult fileUploadResult = new FileUploadResult
                {
                    LocalFilePath = _localFileName,

                    FileName = Path.GetFileName(_localFileName),

                    FileLength = new FileInfo(_localFileName).Length
                };

                // update database
                movie.Image = fileUploadResult.FileName;
                movieService.UpdateMovie(movie);

                response = request.CreateResponse(HttpStatusCode.OK, fileUploadResult);
            }

            return response;
        }

        private Movie GetLastAdded()
        {
            return movieService.GetMovies().OrderByDescending(m => m.ID).FirstOrDefault();
        }

        [HttpGet]
        [Route("images/download")]
        public HttpResponseMessage GetImage(HttpRequestMessage request, int movieId)
        {
            HttpResponseMessage response = null;

            var movie = movieService.GetMovie(movieId);
            if (movie == null)
                response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Invalid movie.");
            else
            {
                var path = HttpContext.Current.Server.MapPath("~/Content/images/" + movie.Image);

                if (!File.Exists(path))
                {
                    response = request.CreateErrorResponse(HttpStatusCode.NotFound, "Image do not exist.");
                }
                else
                {
                    // Create response
                    var fStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                    // Serve the file to the client
                    response = new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StreamContent(fStream)
                        };

                    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = Path.GetFileName(fStream.Name)
                        };

                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                }                
            }

            return response;
        }

    }
}