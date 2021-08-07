using BookShop.DAL.Data;
using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using System.Linq;

namespace BookShop.DAL.Repositories
{
    public class BookAuthorRepository : GenericRepository<BookAuthor>, IBookAuthorRepository
    {
        private readonly ApplicationDbContext _db;
        public BookAuthorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
