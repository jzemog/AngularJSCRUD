using Infrastructure.Core;
using Models;
using Repositories;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MovieService(IMovieRepository _movieRepository, IUnitOfWork _unitOfWork)
        {
            this._movieRepository = _movieRepository;
            this._unitOfWork = _unitOfWork;
        }

        public List<Movie> GetMovies()
        {           
            return _movieRepository.GetAll().ToList();
        }

        public Movie GetMovie(int id)
        {
            return _movieRepository.GetSingle(id);
        }

        public void AddMovie(Movie movie)
        {
            _movieRepository.Add(movie);
            _unitOfWork.Commit();
        }

        public void UpdateMovie(Movie movie)
        {
            _movieRepository.Update(movie);
            _unitOfWork.Commit();
        }

        public void DeleteMovie(Movie movie)
        {
            _movieRepository.Delete(movie);
            _unitOfWork.Commit();
        }
    }
}