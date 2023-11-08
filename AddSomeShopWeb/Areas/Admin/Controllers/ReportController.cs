﻿using ABC.DataAccess.Data;
using ABC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq; // Add this using statement for LINQ
using ABC.Models; // Add this using statement for the OrderHeader model

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


            ViewBag.SalesRevenue = salesRevenue;
            ViewBag.NumberOfItemsSold = numberOfItemsSold; 
            ViewBag.Profit = totalProfit;

            return View();
        }
    }
}
