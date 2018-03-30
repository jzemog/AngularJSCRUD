using Infrastructure.Core;
using Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GenreService(IGenreRepository _genreRepository, IUnitOfWork _unitOfWork)
        {
            this._genreRepository = _genreRepository;
            this._unitOfWork = _unitOfWork;
        }

        public List<Genre> GetGenres()
        {
            return _genreRepository.GetAll().ToList();
        }

        public Genre GetGenre(int id)
        {
            return _genreRepository.GetSingle(id);
        }

        public void AddGenre(Genre genre)
        {
            _genreRepository.Add(genre);
            _unitOfWork.Commit();
        }

        public void UpdateGenre(Genre genre)
        {
            _genreRepository.Update(genre);
            _unitOfWork.Commit();
        }

        public void DeleteGenre(Genre genre)
        {
            _genreRepository.Delete(genre);
            _unitOfWork.Commit();
        }
    }
}