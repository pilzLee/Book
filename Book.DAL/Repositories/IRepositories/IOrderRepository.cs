using BookShop.Models.ViewModels;

namespace BookShop.DAL.Repositories.IRepositories
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        void Update(Order order);
    }
}
