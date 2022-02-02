using DatabaseP.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesP.Interface
{
    public interface IGenreService
    {
        Task AddGenre(Genre genre);
        Task UpdateGenre(int id, Genre genre);
        Task<Genre> GetGenreById(int Id);
        Task<List<Genre>> GetAllGenres();
        Task DeleteGenre(int id);
        Task<List<Genre>> MovieGenre(List<int> selectedGenre);
    }
}
