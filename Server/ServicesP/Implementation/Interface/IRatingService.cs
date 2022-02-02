using DatabaseP.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesP.Implementation.Interface
{
    public interface IRatingService
    {
        Task addNewRating(string userId, Rating rating, Rating currentRating);
        Task<double> avergeRate(int id);
        Task<Rating> getRatingById(string userId, Rating rating);
        Task<int> userRate(int id, string userId);
    }
}
