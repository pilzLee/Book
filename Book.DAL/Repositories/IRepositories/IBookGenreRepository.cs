using BookShop.Models.ViewModels;

namespace BookShop.DAL.Repositories.IRepositories
{
    public interface IBookGenreRepository : IGenericRepository<BookGenre>
    {
        BookGenre Get(int bookId, int genreId);
        void Update(BookGenre bookGenre);

        void RemoveAllBookGenreOfABook(int bookId);
    }
}
