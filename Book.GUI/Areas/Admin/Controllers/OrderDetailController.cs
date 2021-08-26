using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderDetailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var orderDetail = new OrderDetail();
            if (id==null)
            {
                return View(orderDetail);
            }

            orderDetail = _unitOfWork.OrderDetail.Get(id.GetValueOrDefault());

            if (orderDetail==null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                if (orderDetail.OrderId == 0)
                {
                    _unitOfWork.OrderDetail.Add(orderDetail);
                }
                else
                {
                    _unitOfWork.OrderDetail.Update(orderDetail);
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(orderDetail);
        }

        #region Call API
        [HttpGet]
        public IActionResult GetAll()
        {
            var orderDetails = _unitOfWork.OrderDetail.GetAll();
            return Json(new { data = orderDetails});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var orderDetail = _unitOfWork.OrderDetail.Get(id);
            if(orderDetail == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _unitOfWork.OrderDetail.Remove(orderDetail);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful!" });
        }
        #endregion
    }
}
