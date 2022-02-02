using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesP.Implementation.Interface
{
    public interface IUserService
    {
        IQueryable<IdentityUser> GetQueryable();
    }
}
