using Microsoft.AspNetCore.Mvc;
using OrderMnagementAPIs.DTOs;
using OrderMnagementAPIs.Models;
using OrderMnagementAPIs.Services;

namespace OrderMnagementAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult AddProduct(ProductsDTO productDto)
        {
            var product = new Products
            {
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Price = productDto.ProductPrice,
                Stock = productDto.ProductStock
            };

            _productService.AddProduct(product);
            return Ok("Product added successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductsDTO productDto)
        {
            var product = new Products
            {
                ProductId = id,
                ProductName = productDto.ProductName,
                Description = productDto.Description,
                Price = productDto.ProductPrice,
                Stock = productDto.ProductStock
            };

            _productService.UpdateProduct(product);
            return Ok("Product updated successfully.");
        }

        [HttpGet]
        public IActionResult GetProducts(string name = null, decimal? minPrice = null, decimal? maxPrice = null, int page = 1, int pageSize = 10)
        {
            var products = _productService.GetProducts(name, minPrice, maxPrice, page, pageSize);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null) return NotFound("Product not found.");
            return Ok(product);
        }
    }

}
