using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;
using OrderMnagementAPIs.Repositories;

namespace OrderMnagementAPIs.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productRepository; // This field holds the repository for product-related database operations.

        public ProductService(IProductsRepository productRepository)
        {
            _productRepository = productRepository; // Initialize the product repository dependency.
        }

        // Adds a new product after validating its price and stock.
        public void AddProduct(Products product)
        {
            if (product.Price <= 0 || product.Stock < 0)
                throw new InvalidOperationException("Invalid product data."); // Validate product data.
            _productRepository.Add(product); // Save the product to the repository.
        }

        // Updates product details after validation.
        public void UpdateProduct(Products product)
        {
            if (product.Price <= 0 || product.Stock < 0)
                throw new InvalidOperationException("Invalid product data."); // Validate product data.
            _productRepository.Update(product); // Update the product in the repository.
        }

        // Retrieves a paginated and filtered list of products.
        public IEnumerable<ProductsDTO> GetProducts(string name, decimal? minPrice, decimal? maxPrice, int page, int pageSize)
        {
            var query = _productRepository.GetAll().AsQueryable(); // Query all products.
            if (!string.IsNullOrEmpty(name))
                query = query.Where(p => p.ProductName.Contains(name)); // Filter by name.
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value); // Filter by minimum price.
            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value); // Filter by maximum price.
            return query.Skip((page - 1) * pageSize).Take(pageSize).Select(p => new ProductsDTO
            {
                ProductId = p.ProductId, // Map product ID.
                ProductName = p.ProductName, // Map product name.
                Description = p.Description, // Map product description.
                ProductPrice = p.Price, // Map product price.
                ProductStock = p.Stock, // Map product stock.
                OverallRating = p.OverallRating // Map product overall rating.
            }).ToList();
        }

        // Retrieves product details by ID.
        public ProductsDTO GetProductById(int id)
        {
            var product = _productRepository.GetById(id); // Fetch the product by ID.
            return product == null ? null : new ProductsDTO
            {
                ProductId = product.ProductId, // Map product ID.
                ProductName = product.ProductName, // Map product name.
                Description = product.Description, // Map product description.
                ProductPrice = product.Price, // Map product price.
                ProductStock = product.Stock, // Map product stock.
                OverallRating = product.OverallRating // Map product overall rating.
            };
        }
    }

}
