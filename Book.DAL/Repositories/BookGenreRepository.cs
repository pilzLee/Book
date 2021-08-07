using BookShop.DAL.Data;
using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using System.Linq;

namespace BookShop.DAL.Repositories
{
    public class BookGenreRepository : GenericRepository<BookGenre>, IBookGenreRepository
    {
        private readonly ApplicationDbContext _db;
        public BookGenreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public BookGenre Get(int bookId, int genreId)
        {
            return _db.BookGenres
                .Where(bg => bg.BookId == bookId && bg.GenreId == genreId)
                .FirstOrDefault();
        }

        public void Update(BookGenre bookGenre)
        {
            var item = _db.BookGenres.FirstOrDefault(bg => 
                bg.BookId == bookGenre.BookId &&
                bg.GenreId == bookGenre.GenreId
            );

            if (item != null)
            {
                item.BookId = bookGenre.BookId;
                item.GenreId = bookGenre.GenreId;
            }
        }

        public void RemoveAllBookGenreOfABook(int bookId)
        {
            var bookGenres = _db.BookGenres.Where(b => b.BookId == bookId);

            if (bookGenres != null)
            {
                foreach (var item in bookGenres)
                {
                    _db.BookGenres.Remove(item);
                }
            }
        }
    }
}
