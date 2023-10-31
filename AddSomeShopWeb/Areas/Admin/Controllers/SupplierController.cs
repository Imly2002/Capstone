using ABC.DataAccess.Data;
using ABC.DataAccess.Repository.IRepository;
using ABC.Models;
using ABC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddSomeShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]

    public class SupplierController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SupplierController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Retrieve the Data from Database
        public IActionResult Index()
        {
            List<Supplier> objSupplierList = _unitOfWork.Supplier.GetAll().ToList();
            return View(objSupplierList);
        }


        //ADD
        public IActionResult Create()
        {
            return View();

        }

        //Post the Data to Database
        [HttpPost]
        public IActionResult Create(Supplier obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Supplier.Add(obj);
                _unitOfWork.Save();
                TempData["toastAdd"] = "Supplier Added successfully";
                return RedirectToAction("Index", "Supplier");
            }

            return View();
        }




        //Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Supplier? supplierFromDb = _unitOfWork.Supplier.Get(u => u.Id == id);
            //Supplier? supplierFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Supplier? supplierFromDb3 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (supplierFromDb == null)
            {
                return NotFound();
            }

            return View(supplierFromDb);

        }

        //Post the Data to Database
        [HttpPost]
        public IActionResult Edit(Supplier obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Supplier.Update(obj);
                _unitOfWork.Save();
                TempData["toastUpd"] = "Supplier updated successfully";
                return RedirectToAction("Index");
            }

            return View();
        }



        //DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Supplier? productFromDb = _unitOfWork.Supplier.Get(u => u.Id == id);

            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);

        }

        //Post the Data to Database
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Supplier? obj = _unitOfWork.Supplier.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Supplier.Remove(obj);
            _unitOfWork.Save();
            TempData["toastDel"] = "Supplier deleted successfully";
            return RedirectToAction("Index", "Supplier");
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Supplier> objSupplierList = _unitOfWork.Supplier.GetAll().ToList();
            return Json(new { data = objSupplierList });
        }
        #endregion
    }
}
