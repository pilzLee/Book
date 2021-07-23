using System;

namespace BookShop.DAL.Repositories.IRepositories
{
    public interface IUnitOfWork:IDisposable
    {
        IAuthorRepository Author { get; }
        IBookAuthorRepository BookAuthor { get; }
        IBookGenreRepository BookGenre { get; }
        IBookRepository Book { get; }
        IGenreRepository Genre { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderRepository Order { get; }
        IPublisherRepository Publisher { get; }

        void Save();
    }
}
