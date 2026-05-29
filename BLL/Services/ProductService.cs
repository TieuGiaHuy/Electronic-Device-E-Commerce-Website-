using BLL.DTOs.Product;
using BLL.Services.Interfaces;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Brand = p.Brand,
                CategoryId = p.CategoryId,
                IsDeleted = p.IsDeleted,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return productDtos;
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                return new ProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Brand = product.Brand,
                    CategoryId = product.CategoryId,
                    IsDeleted = product.IsDeleted,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt
                };
            }
            return null;
        }
        public async Task<ProductDto> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            var newProduct = Product.Create(
                 productCreateDto.ProductName,
                 productCreateDto.Brand,
                 productCreateDto.CategoryId
            );

            var createdProduct = await _productRepository.CreateProductAsync(newProduct);

            return new ProductDto
            {
                ProductId = createdProduct.ProductId,
                ProductName = createdProduct.ProductName,
                Brand = createdProduct.Brand,
                CategoryId = createdProduct.CategoryId,
                IsDeleted = createdProduct.IsDeleted,
                CreatedAt = createdProduct.CreatedAt,
                UpdatedAt = createdProduct.UpdatedAt
            };
        }

        public async Task<bool> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            var product = new Product
            {
                ProductId = productUpdateDto.ProductId
            };

            product.Update(
                productUpdateDto.ProductName,
                productUpdateDto.Brand,
                productUpdateDto.CategoryId
            );
            
            return await _productRepository.UpdateProductAsync(product); ;
        }

        public async Task<bool> DeleteProductAsync(ProductDeleteDto productDeleteDto)
        {
            return await _productRepository.DeleteProductAsync(productDeleteDto.ProductId); ;
        }

        public async Task<List<ProductDto>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Brand = p.Brand,
                CategoryId = p.CategoryId,
                IsDeleted = p.IsDeleted,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return productDtos;
        }

        public async Task<ProductDto?> GetProductWithVariantsAsync(int productVariantId)
        {
            var product = await _productRepository.GetProductWithVariantsAsync(productVariantId);
            if (product != null)
            {
                return new ProductDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Brand = product.Brand,
                    CategoryId = product.CategoryId,
                    IsDeleted = product.IsDeleted,
                    CreatedAt = product.CreatedAt,
                    UpdatedAt = product.UpdatedAt
                };
            }
            return null;
        }

        public async Task<List<ProductDto>> SearchByNameAsync(string keyword)
        {
            var products = await _productRepository.SearchByNameAsync(keyword);
            var productDtos = products.Select(p => new ProductDto
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Brand = p.Brand,
                CategoryId = p.CategoryId,
                IsDeleted = p.IsDeleted,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList(); 
            return productDtos;
        }
    }
}
