using Ajay.PMS.Data;
using Ajay.PMS.Irepository;
using Ajay.PMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using SQLitePCL;

namespace Ajay.PMS.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ICrudServices<Product> _product;
        private readonly ICrudServices<Category> _category;
        private readonly UserManager<UserLogin> _userManager;
        public CategoryController(ApplicationDbContext context, UserManager<UserLogin> userManager,
            ICrudServices<Category> category, ICrudServices<Product> product)
        {
            _product = product;
            _userManager = userManager;
            _category = category;
        }


        public async Task<IActionResult> Index()
        {
            var result = await  _category.GetAllAsync();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            Category category = new Category();

            if (id > 0)
            {
               category = await _category.GetAsync(id);
            }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Category category)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var user = _userManager.FindByIdAsync(userId);

            if (category.Id == 0)
            {

                category.CreatedBy = userId;
                category.CreatedDate = DateTime.Now;
                await _category.InsertAsync(category);
				TempData["success"] = "Data Added Successfully!";
				return RedirectToAction(nameof(Index));
	}
			else
            {
                var categoryinfo = await _category.GetAsync(category.Id);
                categoryinfo.CategoryName = category.CategoryName;
                categoryinfo.Description = category.Description;
                categoryinfo.ModifiedDate = DateTime.Now;
                categoryinfo.ModifiedBy = userId;
                categoryinfo.IsActive = category.IsActive;
                await _category.UpdateAsync(categoryinfo);
				TempData["success"] = "Data Updated Successfully!";
				return RedirectToAction(nameof(Index));
				

			}
		}

            public async Task<IActionResult> Delete(Category category)
            {

                var  categoryinfo = await _category.GetAsync(category.Id);
               _category.Delete(categoryinfo);
			TempData["success"] = "Data Deleted Successfully!";
			return RedirectToAction(nameof(Index));
            }

    }
}
