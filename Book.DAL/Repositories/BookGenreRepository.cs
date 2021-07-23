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
    }
}
