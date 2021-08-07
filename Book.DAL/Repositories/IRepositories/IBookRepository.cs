using BookShop.Models.ViewModels;

namespace BookShop.DAL.Repositories.IRepositories
{
    public interface IBookRepository:IGenericRepository<Book>
    {
        void Update(Book book);
    }
}
