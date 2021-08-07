using BookShop.Models.ViewModels;

namespace BookShop.DAL.Repositories.IRepositories
{
    public interface IAuthorRepository:IGenericRepository<Author>
    {
        void Update(Author author);
    }
}
