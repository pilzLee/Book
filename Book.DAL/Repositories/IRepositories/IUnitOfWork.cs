using System;

namespace BookShop.DAL.Repositories.IRepositories
{
    public interface IUnitOfWork:IDisposable
    {
        IGenreRepository Genre { get; }
        void Save();
    }
}
