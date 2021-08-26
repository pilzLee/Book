using BookShop.Models.ViewModels;

namespace BookShop.DAL.Repositories.IRepositories
{
    public interface IBookAuthorRepository:IGenericRepository<BookAuthor>
    {
        void RemoveAllBookAuthorOfABook(int bookId);
    }
}
