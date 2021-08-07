using BookShop.Models.ViewModels;

namespace BookShop.DAL.Repositories.IRepositories
{
    public interface IPublisherRepository:IGenericRepository<Publisher>
    {
        void Update(Publisher publisher);
    }
}
