using MyShop.DataAccess.Repository;
using MyShop.Web.DTOS;
using MyShop.Web.Models;


namespace MyShop.Business.Services.CategoryService
{
    public class CategoryServices : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void deleteCategory(int id)
        {
            //return categoryRepo.deleteCategory(id);
            var categoryModel = unitOfWork.Category.GetFristOrDefult(x => x.Id == id);
            unitOfWork.Category.Remove(categoryModel);
            unitOfWork.complete();
        }

        public void editCategory(CategoryDTO categoryDTO)
        {
            //map dto to model
            var model = new Category
            {
                Id = categoryDTO.Id,
                Name = categoryDTO.Name,
                Discreption = categoryDTO.Discreption,
            };
            unitOfWork.Category.Edit(model);
            unitOfWork.complete();
        }

        public List<CategoryDTO> getCategories()
        {
            // get categories from database
            var categories = unitOfWork.Category.GetAll();
            // map category model to category dto

            List<CategoryDTO> DTO = new List<CategoryDTO>();
            foreach (var c in categories)
            {
                DTO.Add(
                    new CategoryDTO
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Discreption = c.Discreption,
                        Date = c.Date,
                    }
                    );

            }
            return DTO;
        }

        public CategoryDTO getCategoryById(int id)
        {
            var res = unitOfWork.Category.GetFristOrDefult(x => x.Id == id);
            //map res to DTO

            var dto = new CategoryDTO
            {
                Id = id,
                Name = res.Name,
                Discreption = res.Discreption,
                Date = res.Date,
            };
            return dto;
        }

        public void setCategory(CategoryDTO categoryDTO)
        {
            // map category dto to model
            var model = new Category
            {
                Name = categoryDTO.Name,
                Discreption = categoryDTO.Discreption,
                Date = categoryDTO.Date,
            };
            // send model to repo
            unitOfWork.Category.Add(model);
            unitOfWork.complete();
        }
    }
}
