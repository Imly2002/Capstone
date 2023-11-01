using ABC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddSomeShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]

	public class POSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
