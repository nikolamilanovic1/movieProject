using DatabaseP.models;
using Microsoft.EntityFrameworkCore;
using MoveisAPI;
using ServicesP.Implementation.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesP.Implementation.Services
{
    public class MovieTheaterService : IMovieTheaterService
    {
        private readonly ApplicationDbContext _db;
        public MovieTheaterService(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public async Task<List<MovieTheater>> GetAllMovieTheaters()
        {
            return await _db.MovieTheaters.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<MovieTheater> GetMovieTheaterById(int Id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.MovieTheaters.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<MovieTheater>> MovieTheater(List<int> selectedMovieTheater)
        {
            return await _db.MovieTheaters.Where(x => !selectedMovieTheater.Contains(x.Id)).ToListAsync();
        }

        public async Task AddMovieTheater(MovieTheater movieTheater)
        {
            _db.Add(movieTheater);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateMovieTheater(int id, MovieTheater movieTheater)
        {
            movieTheater.Id = id;
            _db.Entry(movieTheater).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteMovieTheater(int id)
        {
            var movieTheater = await _db.MovieTheaters.FirstOrDefaultAsync(x => x.Id == id);
            _db.Remove(movieTheater);
            await _db.SaveChangesAsync();
        }
    }
}
