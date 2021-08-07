using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookShop.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var author = new Author();
            if (id==null)
            {
                return View(author);
            }

            author = _unitOfWork.Author.Get(id.GetValueOrDefault());

            if (author==null)
            {
                return NotFound();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.Id == 0)
                {
                    _unitOfWork.Author.Add(author);
                }
                else
                {
                    _unitOfWork.Author.Update(author);
                }
                _unitOfWork.Save();

                return RedirectToAction("Index");
            }
            return View(author);
        }

        #region Call API
        [HttpGet]
        public IActionResult GetAll()
        {
            var authors = _unitOfWork.Author.GetAll();
            return Json(new { data = authors});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var author = _unitOfWork.Author.Get(id);
            if(author == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            _unitOfWork.Author.Remove(author);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful!" });
        }
        #endregion
    }
}
