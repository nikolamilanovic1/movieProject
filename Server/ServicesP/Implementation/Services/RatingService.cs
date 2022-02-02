using DatabaseP.models;
using Microsoft.AspNetCore.Http;
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
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _db;

        public RatingService(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public async Task<Rating> getRatingById(string userId, Rating rating)
        {
#pragma warning disable CS8603 // Possible null reference return.
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var currRate = await _db.Ratings.FirstOrDefaultAsync(x => (x.MovieId == rating.MovieId && x.UserId == userId) );
            return currRate;
        }

        public async Task<double> avergeRate(int id)
        {
            return (await _db.Ratings.AnyAsync(x => x.MovieId == id)) ? await _db.Ratings.Where(x => x.MovieId == id).AverageAsync(x => x.Rate) : 0.0; 

        }

        public async Task<int> userRate(int id, string userId)
        {
            var ratingDb = await _db.Ratings.FirstOrDefaultAsync(x => x.MovieId == id && x.UserId == userId);
            return (ratingDb != null)? ratingDb.Rate : 0;
        }

            public async Task addNewRating(string userId, Rating rating, Rating currentRating)
        {
            if(currentRating == null)
            {
                var newRating = new Rating();
                newRating.MovieId = rating.MovieId;
                newRating.Rate = rating.Rate;
                newRating.UserId = userId;
                _db.Add(newRating);
            }
            else
            {
                currentRating.Rate = rating.Rate;
            }         
            await _db.SaveChangesAsync();
        }


    }
}
