using DatabaseP.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesP.Implementation.Interface
{
    public interface IMovieTheaterService
    {
        Task AddMovieTheater(MovieTheater movieTheater);
        Task DeleteMovieTheater(int id);
        Task<List<MovieTheater>> GetAllMovieTheaters();
        Task<MovieTheater> GetMovieTheaterById(int Id);
        Task<List<MovieTheater>> MovieTheater(List<int> selectedMovieTheater);
        Task UpdateMovieTheater(int id, MovieTheater movieTheater);
    }
}
