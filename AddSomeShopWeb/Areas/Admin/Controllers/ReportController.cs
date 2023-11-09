using ABC.DataAccess.Data;
using ABC.Models;
using System.Linq;
using ABC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AddSomeShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class ReportController : Controller
    {
        private readonly AppDBContext _db;

        public ReportController(AppDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Sales Revenue
            double salesRevenue = _db.OrderHeaders
                .Where(order => order.PaymentStatus == "Paid")
                .Sum(order => order.OrderTotal);

            // Calculate Profit
            double totalProfit = _db.OrderHeaders
                .Where(order => order.PaymentStatus == "Paid")
                .SelectMany(order => _db.OrderDetails.Where(detail => detail.OrderHeaderId == order.Id))
                .Sum(detail => detail.Product != null ?
                    (detail.Price - detail.Product.CostPrice) * detail.Count : 0);

            // Number of Items Sold
            int numberOfItemsSold = _db.OrderHeaders
                .Where(order => order.PaymentStatus == "Paid")
                .SelectMany(order => _db.OrderDetails.Where(detail => detail.OrderHeaderId == order.Id))
                .Sum(detail => detail.Count);

            // Total Cost Price
            double totalCostPrice = _db.Products.Sum(product => product.CostPrice);

            // Get the best-selling product
            var bestSellerProduct = GetBestSellerProduct();

            // Add bestSellerProduct details to ViewBag
            ViewBag.BestSellerProduct = bestSellerProduct?.productName ?? "N/A";
            ViewBag.BestSellerQuantitySold = bestSellerProduct != null
                ? _db.OrderDetails
                    .Where(detail => detail.OrderHeader.PaymentStatus == "Paid" && detail.ProductId == bestSellerProduct.Id)
                    .Sum(detail => detail.Count)
                : 0;

            ViewBag.BestSellerTotalPrice = bestSellerProduct != null
                ? _db.OrderDetails
                    .Where(detail => detail.OrderHeader.PaymentStatus == "Paid" && detail.ProductId == bestSellerProduct.Id)
                    .Sum(detail => detail.Count * detail.Price)
                : 0;

            ViewBag.SalesRevenue = salesRevenue;
            ViewBag.NumberOfItemsSold = numberOfItemsSold;
            ViewBag.Profit = totalProfit;
            ViewBag.TotalCostPrice = totalCostPrice;

            ViewBag.BestSellerProduct = bestSellerProduct;

            return View();
        }

        private Product GetBestSellerProduct()
        {
            var bestSellerProductId = _db.OrderHeaders
                .Where(order => order.PaymentStatus == "Paid")
                .SelectMany(order => order.OrderDetails)
                .GroupBy(detail => detail.ProductId)
                .AsEnumerable() // Switch to client-side evaluation
                .OrderByDescending(group => group.Sum(detail => detail.Count))
                .FirstOrDefault()?.Key;

            var bestSellerProduct = _db.Products.FirstOrDefault(product => product.Id == bestSellerProductId);

            return bestSellerProduct;
        }

    }
}
