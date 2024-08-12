using MyShop.Entity.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Business.Services.UsersService
{
    public interface IUserService
    {
        public List<ApplicationUserDTO> getAllUserWithoutAuthenticatedUser(string id);
        public void lockAndUnlockUser(string id);
        public void deleteUser(string id);
    }
}
