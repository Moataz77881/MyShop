using MyShop.DataAccess.Repository;
using MyShop.Entity.DTOS;


namespace MyShop.Business.Services.UsersService
{
	public class UserService : IUserService
    {
        private readonly IUsers user;
		private readonly IUnitOfWork unitOfWork;

		public UserService(IUsers user, IUnitOfWork unitOfWork)
        {
            this.user = user;
			this.unitOfWork = unitOfWork;
		}

		public void deleteUser(string id)
		{
            var userEntity = unitOfWork.users.GetFristOrDefult(x => x.Id == id);
            unitOfWork.users.Remove(userEntity);
            unitOfWork.complete();
		}

		public List<ApplicationUserDTO> getAllUserWithoutAuthenticatedUser(string id)
        {
            var usersModel = user.getUsersWithoutAuthenticatedUser(id);

            List<ApplicationUserDTO> dTOs = new List<ApplicationUserDTO>();

            foreach (var user in usersModel) 
            {
                dTOs.Add(new ApplicationUserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Address = user.Address,
                    City = user.City,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    LockoutEnd = user.LockoutEnd
                });
            } 
            return dTOs;
            
        }

		public void lockAndUnlockUser(string id)
		{
            var user = unitOfWork.users.GetFristOrDefult(x => x.Id == id);

            if (user != null && user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
                unitOfWork.complete();
            }
            else if (user != null && user.LockoutEnd == null || user.LockoutEnd < DateTime.Now)
			{
                user.LockoutEnd = DateTime.Now.AddHours(4);
                unitOfWork.complete();
            }
		}
	}
}
