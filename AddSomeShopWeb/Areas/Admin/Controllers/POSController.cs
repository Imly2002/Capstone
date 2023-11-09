using ABC.DataAccess.Data;
using ABC.DataAccess.Repository.IRepository;
using ABC.Models;
using ABC.Models.ViewModels;
using ABC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;

namespace AddSomeShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

    public class POSController : Controller
    {

        private static List<OrderHeader> inProcessOrders = new List<OrderHeader>();
        private readonly AppDBContext _db;
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderVM OrderVM { get; set; }

        public POSController(AppDBContext context, IUnitOfWork unitOfWork)
        {
            _db = context;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? id)
        {
            ViewBag.products = new SelectList(_db.Products, "Id", "ProductName");
            return View();
        }

        // Add a new action to fetch product data as JSON for Select2
        [HttpGet]
        public IActionResult GetPakyu(string term)
        {
            var products = _db.Products
                .Where(p => p.productName.Contains(term) && p.StockQuantity > 0)
                .Select(p => new { id = p.Id, text = p.productName, retailPrice = p.RetailPrice, img = p.ImageUrl })
                .ToList();

            return Json(products);
        }

        [HttpGet]
        public IActionResult GetRetailPrice(string productName)
        {
            var retailPrice = _db.Products
                .Where(p => p.productName == productName)
                .Select(p => p.RetailPrice)
                .FirstOrDefault();

            // Return the retail price as JSON
            return Json(retailPrice);
        }

        [HttpGet]
        public IActionResult CheckStock(string productName, int quantity)
        {
            var currentStock = _db.Products
                .Where(p => p.productName == productName)
                .Select(p => p.StockQuantity)
                .FirstOrDefault();

            // Check if the quantity is less than or equal to the current stock
            bool isStockAvailable = quantity <= currentStock;

            // Return the result as JSON
            return Json(isStockAvailable);
        }
    }
}
