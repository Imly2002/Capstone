using ABC.DataAccess.Data;
using ABC.DataAccess.Repository.IRepository;
using ABC.Models;
using ABC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddSomeShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class PurchaseOrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PurchaseOrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Retrieve the Data from Database
        public IActionResult Index()
        {
            List<PurchaseOrder> objPurchaseOrderList = _unitOfWork.PurchaseOrder.GetAll().ToList();
            return View(objPurchaseOrderList);
        }


        //ADD
        public IActionResult Create()
        {
            return View();

        }

        //Post the Data to Database
        [HttpPost]
        public IActionResult Create(PurchaseOrder obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.PurchaseOrder.Add(obj);
                _unitOfWork.Save();
                TempData["toastAdd"] = "PurchaseOrder Added successfully";
                return RedirectToAction("Index", "PurchaseOrder");
            }

            return View();
        }



        //Edit
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            PurchaseOrder? purchaseorderFromDb = _unitOfWork.PurchaseOrder.Get(u => u.Id == id);
            //PurchaseOrder? purchaseorderFromDb1 = _db.PurchaseOrder.FirstOrDefault(u=>u.Id==id);
            //PurchaseOrder? purchaseorderFromDb3 = _db.PurchaseOrder.Where(u => u.Id == id).FirstOrDefault();

            if (purchaseorderFromDb == null)
            {
                return NotFound();
            }

            return View(purchaseorderFromDb);

        }

        ////Post the Data to Database
        //[HttpPost]
        //public IActionResult Edit(PurchaseOrder obj)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.PurchaseOrder.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["toastUpd"] = "PurchaseOrder updated successfully";
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}



        //DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            PurchaseOrder? purchaseorderFromDb = _unitOfWork.PurchaseOrder.Get(u => u.Id == id);

            if (purchaseorderFromDb == null)
            {
                return NotFound();
            }
            return View(purchaseorderFromDb);

        }

        //Post the Data to Database
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            PurchaseOrder? obj = _unitOfWork.PurchaseOrder.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.PurchaseOrder.Remove(obj);
            _unitOfWork.Save();
            TempData["toastDel"] = "PurchaseOrder deleted successfully";
            return RedirectToAction("Index", "PurchaseOrder");
        }



        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<PurchaseOrder> objPurchaseOrderList = _unitOfWork.PurchaseOrder.GetAll().ToList();
            return Json(new { data = objPurchaseOrderList });
        }
        #endregion
    }
}
