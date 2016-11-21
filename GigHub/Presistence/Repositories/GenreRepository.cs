using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
using GigHub.Core;

namespace GigHub.Presistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly IApplicationDbContext _context;

        public GenreRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}