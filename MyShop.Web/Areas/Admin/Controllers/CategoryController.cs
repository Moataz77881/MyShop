using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyShop.Business.Services.CategoryService;
using MyShop.Entity;
using MyShop.Web.DTOS;

namespace MyShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        [Authorize(Roles =nameof(RolesName.Admin)+","+nameof(RolesName.Editor))]
        public IActionResult getCategories()
        {
            var categories = categoryService.getCategories();
            return View(categories);
        }

        [HttpGet]
		[Authorize(Roles = nameof(RolesName.Admin) + "," + nameof(RolesName.Editor))]

		public IActionResult setCategory()
        {
            return View();
        }
        [HttpPost]
		[Authorize(Roles = nameof(RolesName.Admin) + "," + nameof(RolesName.Editor))]

		public IActionResult setCategory(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                categoryService.setCategory(categoryDTO);
                TempData["setCategory"] = "Data Has Created Successfully";
                return RedirectToAction("getCategories");
            }
            return View(categoryDTO);
        }
        [HttpGet]
		[Authorize(Roles = nameof(RolesName.Admin) + "," + nameof(RolesName.Editor))]

		public IActionResult editCategory(int id)
        {
            var category = categoryService.getCategoryById(id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Authorize(Roles = nameof(RolesName.Admin) + "," + nameof(RolesName.Editor))]

		public IActionResult editCategory(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                categoryService.editCategory(categoryDTO);
                TempData["editCategory"] = "Data has Edited Successfully";
                return RedirectToAction("getCategories");
            }
            return View(categoryDTO);
        }

        [HttpGet]
        [Authorize(Roles =nameof(RolesName.Admin))]
        public IActionResult deleteCategory(int id)
        {
            categoryService.deleteCategory(id);
            TempData["deleteCategory"] = "Data Has Deleted Successfully";
            return RedirectToAction("getCategories");
        }
    }
}
