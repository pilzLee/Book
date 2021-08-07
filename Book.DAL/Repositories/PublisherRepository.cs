using BookShop.DAL.Data;
using BookShop.DAL.Repositories.IRepositories;
using BookShop.Models.ViewModels;
using System.Linq;

namespace BookShop.DAL.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        private readonly ApplicationDbContext _db;
        public PublisherRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Publisher publisher)
        {
            var item = _db.Publishers.FirstOrDefault(p => p.Id == publisher.Id);

            if (item != null)
            {
                item.PublisherName = publisher.PublisherName;
            }

        }
    }
}
