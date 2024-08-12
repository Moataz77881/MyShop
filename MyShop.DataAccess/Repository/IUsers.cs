using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using MyShop.Entity.Models;

namespace MyShop.DataAccess.Repository
{
    public interface IUsers : IGenericRepository<ApplicationUser>
    {
        public List<ApplicationUser> getUsersWithoutAuthenticatedUser(string id);
    }
}
