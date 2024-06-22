using Ajay.PMS.Data;
using Ajay.PMS.Irepository;
using Ajay.PMS.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Ajay.PMS.Controllers
{
    [Authorize]
    public class ProductController : Controller
        
    {
        private readonly ApplicationDbContext _context;
        private readonly ICrudServices<Product> _productinfo;

        private readonly UserManager<UserLogin> _userManager;
        private readonly ICrudServices<Category> _categoryinfo;

        public ProductController(ApplicationDbContext context, UserManager<UserLogin> userManager, 
            ICrudServices<Category> categoryinfo, ICrudServices<Product> productinfo)
        {
            _context = context;
            _userManager = userManager;
            _categoryinfo = categoryinfo;
            _productinfo = productinfo;
        }

        
        public async Task<IActionResult> Index(string searchString, int? selectedProductId)
        {
            ViewBag.CategoryInfo = await _categoryinfo.GetAllAsync(x => x.IsActive == true);
            var productInfo = await _productinfo.GetAllAsync();

            if (!String.IsNullOrEmpty(searchString))
            {
                 productInfo =  productInfo.Where(x => x.ProductName.Contains(searchString)).ToList();
            }

            if (selectedProductId!=null)
            {
                productInfo = productInfo.Where(x => x.CategoryId== selectedProductId).ToList();
            }
            return View(productInfo);
        }

        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            ViewBag.CategoryInfos = await _categoryinfo.GetAllAsync(p => p.IsActive == true);
            Product pro = new Product();
            if (id > 0)
            {
                pro = await _productinfo.GetAsync(x => x.Id == id);
            }
            return View(pro);
        }

        [HttpPost]
        public async Task<IActionResult> AddEdit(Product product)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            ViewBag.CategoryInfo = await _categoryinfo.GetAllAsync(x => x.IsActive == true);

            if (product.ProductFile != null)
            {
                var fileDirectory = "wwwroot/ProductImage";

                if (!Directory.Exists(fileDirectory))
                {
                    Directory.CreateDirectory(fileDirectory);
                }

                var uniqueFileName = $"{Guid.NewGuid()}_{product.ProductFile.FileName}";
                var filePath = Path.Combine(fileDirectory, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ProductFile.CopyToAsync(fileStream);
                }

                product.ProductUrl = $"ProductImage/{uniqueFileName}";
            }

            if (product.Id == 0)
            {
                product.CreatedDate = DateTime.Now;
                product.CreatedBy = userId;
				await _productinfo.InsertAsync(product);
				TempData["success"] = "Data Updated Successfully!";
			}
            else
            {
                var productInfo = await _productinfo.GetAsync(x => x.Id == product.Id);
                if (productInfo == null)
                {
                    return NotFound();
                }

                productInfo.ProductName = product.ProductName;
                productInfo.Manufacturer = product.Manufacturer;
                productInfo.ProductionDate = product.ProductionDate;
                productInfo.ExpiryDate = product.ExpiryDate;
                productInfo.BatchNo = product.BatchNo;
                productInfo.Rate = product.Rate;
                productInfo.CategoryId = product.CategoryId;
                productInfo.ProductUrl = product.ProductUrl ?? productInfo.ProductUrl;
                productInfo.IsActive = product.IsActive;
                productInfo.ModifiedDate = DateTime.Now;
                productInfo.ModifiedBy = userId;

                 await _productinfo.UpdateAsync(productInfo);
				TempData["success"] = "Data Updated Successfully!";
			}
			
			return RedirectToAction(nameof(Index));
			

		}

        public async Task<IActionResult> Detail(int id)
        {
            ViewBag.CategoryInfo = await _categoryinfo.GetAllAsync(x => x.IsActive == true);
            var product = await _productinfo.GetAsync(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }
	
			return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productinfo = await _productinfo.GetAsync(a => a.Id == id);
             _productinfo.Delete(productinfo);
			TempData["success"] = "Data Deleted Successfully!";
			return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //[Route("/api/Product/getproduct")]
        //public async Task<IActionResult> GetProduct(int productinfo)
        //{
        //    var productinfos = await _productinfo.GetAsync(productinfo);
        //    return Json(new { productinfos });
        //}
    }
}
