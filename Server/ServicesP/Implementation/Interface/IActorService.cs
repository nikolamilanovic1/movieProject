using DatabaseP.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesP.Implementation.Interface
{
    public interface IActorService
    {
        Task addActor(Actor actor);
        Task DeleteActor(int id);
        Task<Actor> GetActorById(int Id);
        Task<List<Actor>> GetAllActors();
        Task<List<Actor>> GetAllActorsByName(string name);
        IQueryable<Actor> GetQueryable();
        Task SaveActor();
    }
}
