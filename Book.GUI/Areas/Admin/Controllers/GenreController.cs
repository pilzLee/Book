using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using BookShop.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticRole.RoleAdmin + "," + StaticRole.RoleEmployee)]
    public class GenreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var genre = new Genre();
            if (id==null)
            {
                return View(genre);
            }

            genre = _unitOfWork.Genre.Get(id.GetValueOrDefault());

            if (genre==null)
            {
                return NotFound();
            }

            return View(genre);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (genre.Id == 0)
                {
                    _unitOfWork.Genre.Add(genre);
                }
                else
                {
                    _unitOfWork.Genre.Update(genre);
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(genre);
        }

        #region Call API
        [HttpGet]
        public IActionResult GetAll()
        {
            var genres = _unitOfWork.Genre.GetAll();
            return Json(new { data = genres});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var genre = _unitOfWork.Genre.Get(id);
            if(genre == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _unitOfWork.Genre.Remove(genre);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful!" });
        }
        #endregion
    }
}
