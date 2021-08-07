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
            Author = new AuthorRepository(_db);
            BookAuthor = new BookAuthorRepository(_db);
            BookGenre = new BookGenreRepository(_db);
            Book = new BookRepository(_db);
            Genre = new GenreRepository(_db);
            Order = new OrderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            Publisher = new PublisherRepository(_db);
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
