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
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _db;

        public MovieService(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public async Task<Movie> GetMovieById(int Id)
        {
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            return await _db.Movies
                .Include(x => x.MoviesGenres).ThenInclude(x => x.Genre)
                .Include(x => x.MovieTheatersMovies).ThenInclude(x => x.MovieTheater)
                .Include(x => x.MoviesActors).ThenInclude(x => x.Actor)
                .FirstOrDefaultAsync(x => x.Id == Id);

        }

        public async Task<List<Movie>> GetAllMoviesByReleaseDate(DateTime today,int top)
        {
            return await _db.Movies.Where(x => x.ReleaseDate > today).OrderBy(x => x.ReleaseDate).Take(top).ToListAsync();
        }

        public async Task<List<Movie>> GetAllMoviesInTheaters(int top)
        {
            return await _db.Movies.Where(x => x.InTheaters).OrderBy(x => x.ReleaseDate).Take(top).ToListAsync();
        }

        public async Task AddMovie(Movie movie)
        {
            AnnotateActorsOrder(movie);
            _db.Add(movie);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateMovie(Movie movie)
        {
            AnnotateActorsOrder(movie);
            _db.Update(movie);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Movie movie)
        {
            AnnotateActorsOrder(movie);
            _db.Remove(movie);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Movie> GetQueryable()
        {
            var queryable = _db.Movies.AsQueryable();
            return queryable;
        }


        public void AnnotateActorsOrder(Movie movie)
        {
            if(movie.MoviesActors != null)
            {
                for(int i=0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        }

    }
}
