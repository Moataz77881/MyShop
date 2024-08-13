using Microsoft.EntityFrameworkCore;
using MyShop.DataAccess.Repository;
using MyShop.Entity.Models;
using MyShop.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Implementation
{
    public class UsersRepository : GenericRepository<ApplicationUser> ,IUsers
    {
        private readonly DbSet<ApplicationUser> DbSet;
        public UsersRepository(ApplicationDbContext context) :base(context)
        {
            DbSet = context.Set<ApplicationUser>();
        }
        public List<ApplicationUser> getUsersWithoutAuthenticatedUser(string id)
        {
            IQueryable<ApplicationUser> query = DbSet;
            query = query.Where(x => x.Id != id);
            return query.ToList();
        }
    }
}
