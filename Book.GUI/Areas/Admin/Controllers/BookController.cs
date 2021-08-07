using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

namespace BookShop.GUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var bookVM = new BookVM();
            bookVM.Book = new Book();
            bookVM.Genres = _unitOfWork.Genre.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            bookVM.Authors = _unitOfWork.Author.GetAll().Select(i => new SelectListItem
            {
                Text = i.FullName,
                Value = i.Id.ToString()
            });
            bookVM.Publishers = _unitOfWork.Publisher.GetAll().Select(i => new SelectListItem
            {
                Text = i.PublisherName,
                Value = i.Id.ToString()
            });


            if (id == null)
            {
                return View(bookVM);
            }

            //// Eager load both BookGenre and BookAuthor
            bookVM.Book = _unitOfWork.Book.GetAll(
                    includeProperties: "BookGenres.Genre,BookAuthors.Author"
                    )
                .Where(b => b.Id == id.GetValueOrDefault())
                .FirstOrDefault();

            bookVM.SelectedGenres = bookVM.Book.BookGenres.Select(g => g.GenreId);
            bookVM.SelectedAuthors = bookVM.Book.BookAuthors.Select(a => a.AuthorId);

            if (bookVM.Book == null)
            {
                return NotFound();
            }

            return View(bookVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM bookVM)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var upload = Path.Combine(webRootPath, @"images\books");
                    var fileExtention = Path.GetExtension(files[0].FileName);

                    if (bookVM.Book.ImageUrl != null)
                    {
                        var imgPath = Path.Combine(webRootPath, bookVM.Book.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(imgPath))
                        {
                            System.IO.File.Delete(imgPath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(upload, fileName + fileExtention), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    bookVM.Book.ImageUrl = @"\images\books\" + fileName + fileExtention;
                }
                else
                {
                    //// Update when not change the img
                    if (bookVM.Book.Id != 0)
                    {
                        var book = _unitOfWork.Book.Get(bookVM.Book.Id);
                        bookVM.Book.ImageUrl = book.ImageUrl;
                    }
                }

                if (bookVM.Book.Id == 0)
                {
                    _unitOfWork.Book.Add(bookVM.Book);
                    _unitOfWork.Save();

                    AddBookGenreAndAuthor(bookVM);
                }
                else
                {
                    //// First delete the existing genre and author
                    DeleteBookGenreAndAuthor(bookVM.Book.Id);

                    //// Update the book
                    _unitOfWork.Book.Update(bookVM.Book);

                    //// Readd new genre and author
                    AddBookGenreAndAuthor(bookVM);
                }


                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(bookVM);
        }

        /// <summary>
        /// This method use to add bookgenre and bookauthor
        /// </summary>
        /// <param name="bookVM">Book View Model</param>
        private void AddBookGenreAndAuthor(BookVM bookVM)
        {
            //// Add list book author
            foreach (var item in bookVM.SelectedAuthors)
            {
                _unitOfWork.BookAuthor.Add(new BookAuthor(bookVM.Book.Id, item));
            }

            //// Add list book genre
            foreach (var item in bookVM.SelectedGenres)
            {
                _unitOfWork.BookGenre.Add(new BookGenre(bookVM.Book.Id, item));
            }
        }

        /// <summary>
        /// This method use to remove bookgenre and book author
        /// </summary>
        /// <param name="bookId">Book Id</param>
        private void DeleteBookGenreAndAuthor(int bookId)
        {
            _unitOfWork.BookAuthor.RemoveAllBookAuthorOfABook(bookId);

            _unitOfWork.BookGenre.RemoveAllBookGenreOfABook(bookId);
        }

        #region Call API
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _unitOfWork.Book
                .GetAll(
                includeProperties:
                "BookAuthors.Author,BookGenres.Genre,Publisher");

            return Json(new { data = books });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var book = _unitOfWork.Book.Get(id);
            if (book == null)
            {
                return Json(new { success = false, message = "Error while deleting!" });
            }

            if (book.ImageUrl != null)
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                var imgPath = Path.Combine(webRootPath, book.ImageUrl.TrimStart('\\'));

                if (System.IO.File.Exists(imgPath))
                {
                    System.IO.File.Delete(imgPath);
                }
            }

            _unitOfWork.Book.Remove(book);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful!" });
        }
        #endregion
    }
}
