using BookShop.DAL.Data;
using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using System.Linq;

namespace BookShop.DAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Order order)
        {
            var item = _db.Orders.FirstOrDefault(o => o.Id == o.Id);

            if (item != null)
            {
                // 
            }

        }
    }
}
