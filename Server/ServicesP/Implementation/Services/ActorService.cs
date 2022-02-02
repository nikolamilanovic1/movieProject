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
    public class ActorService : IActorService
    {
        private readonly ApplicationDbContext _db;

        public ActorService(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public async Task<List<Actor>> GetAllActors()
        {
            return await _db.Actors.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Actor> GetActorById(int Id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Actors.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<Actor>> GetAllActorsByName(string name)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _db.Actors.Where(x => x.Name.Contains(name)).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task addActor(Actor actor)
        {
            _db.Actors.Add(actor);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteActor(int id)
        {
            var actor = await _db.Actors.FirstOrDefaultAsync(x => x.Id == id);
            _db.Remove(actor);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Actor> GetQueryable()
        {
            var queryable =  _db.Actors.AsQueryable();
            return queryable;
        }

        public async Task SaveActor()
        {
            await _db.SaveChangesAsync();
        }

    }
}
