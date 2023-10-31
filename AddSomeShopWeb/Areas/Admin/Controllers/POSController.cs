using Microsoft.AspNetCore.Mvc;

namespace AddSomeShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class POSController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
