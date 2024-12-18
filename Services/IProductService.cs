using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;

namespace OrderMnagementAPIs.Services
{
    public interface IProductService
    {
        void AddProduct(Products product);
        ProductsDTO GetProductById(int id);
        IEnumerable<ProductsDTO> GetProducts(string name, decimal? minPrice, decimal? maxPrice, int page, int pageSize);
        void UpdateProduct(Products product);
    }
}