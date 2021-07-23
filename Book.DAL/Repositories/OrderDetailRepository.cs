using BookShop.DAL.Data;
using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using System.Linq;

namespace BookShop.DAL.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail orderDetail)
        {
            var item = _db.OrderDetails.FirstOrDefault(od => od.OrderId == orderDetail.OrderId);

            if (item != null)
            {
                item.Quantity = item.Quantity;
            }

        }
    }
}
