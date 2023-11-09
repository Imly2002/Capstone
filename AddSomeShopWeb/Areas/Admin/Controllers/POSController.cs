using ABC.DataAccess.Data;
using ABC.DataAccess.Repository.IRepository;
using ABC.Models;
using ABC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // Add this namespace for Entity Framework

namespace AddSomeShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class POSController : Controller
    {
        private readonly AppDBContext _db;

        public POSController(AppDBContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? id)
        {
            ViewBag.products = new SelectList(_db.Products, "Id", "ProductName");
            return View();
        }

        // Add a new action to fetch product data as JSON for Select2
        [HttpGet]
        public IActionResult GetProducts(string term)
        {
            var products = _db.Products
                .Where(p => p.productName.Contains(term))
                .Select(p => new { id = p.Id, text = p.productName, retailPrice = p.RetailPrice, img = p.ImageUrl })
                .ToList();


            return Json(products);
        }

    }
}
