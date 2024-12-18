using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Repositories
{
    public interface IProductsRepository
    {
        void Add(Products product);
        void Delete(Products product);
        IEnumerable<Products> GetAll();
        Products GetById(int id);
        void Update(Products product);
    }
}