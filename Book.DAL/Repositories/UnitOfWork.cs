using BookShop.DAL.Data;
using BookShop.DAL.Repositories.IRepositories;

namespace BookShop.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Genre = new GenreRepository(_db);
        }

        public IAuthorRepository Author { get; private set; }

        public IBookAuthorRepository BookAuthor { get; private set; }

        public IBookGenreRepository BookGenre { get; private set; }

        public IBookRepository Book { get; private set; }

        public IGenreRepository Genre { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public IOrderRepository Order { get; private set; }

        public IPublisherRepository Publisher { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
