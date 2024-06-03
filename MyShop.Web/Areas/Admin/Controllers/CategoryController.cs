using Microsoft.AspNetCore.Mvc;
using MyShop.Business.CategoryServes;
using MyShop.Web.DTOS;

namespace MyShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult getCategories()
        {
            var categories = categoryService.getCategories();
            return View(categories);
        }

        [HttpGet]
        public IActionResult setCategory()
        {
            return View();
        }
        [HttpPost]
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
        public IActionResult editCategory(int id)
        {
            var category = categoryService.getCategoryById(id);
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public IActionResult deleteCategory(int id)
        {
            categoryService.deleteCategory(id);
            TempData["deleteCategory"] = "Data Has Deleted Successfully";
            return RedirectToAction("getCategories");
        }
    }
}
