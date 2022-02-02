using DatabaseP.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesP.Implementation.Interface
{
    public interface IMovieService
    {
        Task AddMovie(Movie movie);
        void AnnotateActorsOrder(Movie movie);
        Task Delete(Movie movie);
        Task<List<Movie>> GetAllMoviesByReleaseDate(DateTime today, int top);
        Task<List<Movie>> GetAllMoviesInTheaters(int top);
        Task<Movie> GetMovieById(int Id);
        IQueryable<Movie> GetQueryable();
        Task UpdateMovie(Movie movie);
    }
}
