using BookShop.Models.ViewModels;

namespace BookShop.DAL.Repositories.IRepositories
{
    public interface IGenreRepository:IGenericRepository<Genre>
    {
        void Update(Genre genre);
    }
}
