using BookShop.DAL.Data;
using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using System.Linq;

namespace BookShop.DAL.Repositories
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Book book)
        {
            var item = _db.Books.FirstOrDefault(b => b.Id == book.Id);

            if (item != null)
            {
                item.ISBN = book.ISBN;
                item.Title = book.Title;
                item.Price = book.Price;
                item.Publisher = book.Publisher;
                item.Edition = book.Edition;
                item.AvailableQuantity = book.AvailableQuantity;
                item.Description = book.Description;
                item.BookAuthors = book.BookAuthors;
                item.BookGenres = book.BookGenres;
                item.OrderDetails = book.OrderDetails;
            }

        }
    }
}
