using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var order = new Order();
            if (id==null)
            {
                return View(order);
            }

            order = _unitOfWork.Order.Get(id.GetValueOrDefault());

            if (order==null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.Id == 0)
                {
                    _unitOfWork.Order.Add(order);
                }
                else
                {
                    _unitOfWork.Order.Update(order);
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(order);
        }

        #region Call API
        [HttpGet]
        public IActionResult GetAll()
        {
            var orders = _unitOfWork.Order.GetAll();
            return Json(new { data = orders});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var order = _unitOfWork.Order.Get(id);
            if(order == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _unitOfWork.Order.Remove(order);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful!" });
        }
        #endregion
    }
}
