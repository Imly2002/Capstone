using ABC.DataAccess.Data;
using ABC.DataAccess.Repository.IRepository;
using ABC.Models;
using ABC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewBag.proucts = new SelectList(_db.Products, "Id", "productName");
            return View();
        }
    }
}
