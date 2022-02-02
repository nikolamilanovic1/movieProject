using Microsoft.AspNetCore.Identity;
using MoveisAPI;
using ServicesP.Implementation.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesP.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public IQueryable<IdentityUser> GetQueryable()
        {
            var queryable = _db.Users.AsQueryable();
            return queryable;
        }
    }
}
