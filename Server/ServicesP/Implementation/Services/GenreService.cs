using DatabaseP.models;
using Microsoft.EntityFrameworkCore;
using MoveisAPI;
using ServicesP.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesP.Services
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _db;
        public GenreService(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _db.Genres.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Genre> GetGenreById(int Id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Genres.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Genre>> MovieGenre(List<int> selectedGenre)
        {
            return await _db.Genres.Where(x => !selectedGenre.Contains(x.Id)).ToListAsync();
        }

        public async Task AddGenre(Genre genre)
        {
            _db.Add(genre);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateGenre(int id, Genre genre)
        {
            genre.Id = id;
            _db.Entry(genre).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteGenre(int id)
        {
            var genre = await _db.Genres.FirstOrDefaultAsync(x => x.Id == id);
            _db.Remove(genre);
            await _db.SaveChangesAsync();
        }

       
    }
}
