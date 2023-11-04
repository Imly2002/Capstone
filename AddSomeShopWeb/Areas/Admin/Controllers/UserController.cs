using ABC.DataAccess.Data;
using ABC.DataAccess.Repository.IRepository;
using ABC.Models;
using ABC.Models.ViewModels;
using ABC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AddSomeShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = SD.Role_Admin)]

	public class UserController : Controller
    {
        private readonly AppDBContext _db;
        public UserController(AppDBContext db)
        {
            _db = db;
        }

        //Retrieve the Data from Database
        public IActionResult Index()
        {
            return View();
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _db.ApplicationUsers.Include(u=>u.Name).ToList();
            return Json(new {data = objUserList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {

            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
