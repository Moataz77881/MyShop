using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Business.Services.UsersService;
using MyShop.Entity;
using MyShop.Entity.DTOS;
using MyShop.Web.Data;
using System.Security.Claims;

namespace MyShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(RolesName.Admin))]
    public class UsersController : Controller
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }
        [HttpGet]
        public IActionResult allUsers()
        {
            var ClaimsIdintity = (ClaimsIdentity)User.Identity;
            var claim = ClaimsIdintity.FindFirst(ClaimTypes.NameIdentifier);
            string userid = claim.Value;

            return View(userService.getAllUserWithoutAuthenticatedUser(userid));
        }
        [HttpGet]
        public IActionResult deleteUser(string id) 
        {
			userService.deleteUser(id);
			TempData["deleteUser"] = "User Deleted";
			return RedirectToAction("allUsers", "Users");
        }
        public IActionResult LockUnlockUsers(string id) 
        {
            userService.lockAndUnlockUser(id);
            return RedirectToAction("allUsers", "Users", new { area = "Admin" });
        }
    }
}
