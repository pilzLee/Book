using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using BookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticRole.RoleAdmin + "," + StaticRole.RoleEmployee)]
    public class PublisherController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublisherController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var publisher = new Publisher();
            if (id==null)
            {
                return View(publisher);
            }

            publisher = _unitOfWork.Publisher.Get(id.GetValueOrDefault());

            if (publisher==null)
            {
                return NotFound();
            }

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                if (publisher.Id == 0)
                {
                    _unitOfWork.Publisher.Add(publisher);
                }
                else
                {
                    _unitOfWork.Publisher.Update(publisher);
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        #region Call API
        [HttpGet]
        public IActionResult GetAll()
        {
            var publishers = _unitOfWork.Publisher.GetAll();
            return Json(new { data = publishers});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var publisher = _unitOfWork.Publisher.Get(id);
            if(publisher == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _unitOfWork.Publisher.Remove(publisher);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful!" });
        }
        #endregion
    }
}
