using ORM_Mini_Project.DTOs.ProductDtos;
using ORM_Mini_Project.Exceptions;
using ORM_Mini_Project.Models;
using ORM_Mini_Project.Repositories.Implementations;
using ORM_Mini_Project.Repositories.Interfaces;
using ORM_Mini_Project.Services.Interfaces;

namespace ORM_Mini_Project.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService()
    {
        _productRepository = new ProductRepository();
    }

    public async Task CreateAsync(ProductPostDto productDto)
    {
        var isProductExist = await _productRepository.IsExistAsync(x => x.Name == productDto.Name);
        if (isProductExist)
            throw new AlreadyExistException("Product with given name already exist");
        if (string.IsNullOrEmpty(productDto.Name) || productDto.Price < 0)
            throw new InvalidProductException("Name cannot be empty and price should be positive number");

        Product product = new()
        {
            Name = productDto.Name,
            Stock = productDto.Stock,
            Description = productDto.Description,
            Price = productDto.Price,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };
        await _productRepository.CreateAsync(product);
        await _productRepository.SaveAllChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _getByIdAsync(id);
        _productRepository.Delete(product);
        await _productRepository.SaveAllChangesAsync();
    }

    public async Task UpdateAsync(ProductPutDto newProductDto)
    {
        var product = await _getByIdAsync(newProductDto.Id);

        if (newProductDto.Price < 0 || newProductDto.Stock < 0)
            throw new InvalidProductException("Price and Stoc count should be positive");

        var isProductExist = await _productRepository.IsExistAsync(x => x.Name == newProductDto.Name && x.Id != newProductDto.Id);
        if (isProductExist)
            throw new AlreadyExistException("Product with given name already exist");

        product.Name = newProductDto.Name;
        product.Stock = newProductDto.Stock;
        product.Price = newProductDto.Price;
        product.UpdatedDate = DateTime.UtcNow;
        product.Description = newProductDto.Description;

        _productRepository.Update(product);
        await _productRepository.SaveAllChangesAsync();

    }

    public async Task<List<ProductGetDto>> GetAllAsync()
    {
        var products = await _productRepository.GetAllAsync("OrderDetails.Orders");

        List<ProductGetDto> productsList = new List<ProductGetDto>();

        products.ForEach(product =>
        {
            ProductGetDto productGetDto = new()
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Description = product.Description,
            };
            productsList.Add(productGetDto);
        });

        return productsList;
    }

    public async Task<ProductGetDto> GetByIdAsync(int id)
    {
        var product = await _getByIdAsync(id);

        ProductGetDto productGetDto = new()
        {
            Id = product.Id,
            Name = product.Name,
            Stock = product.Stock,
            Price = product.Price,
            Description = product.Description,
        };

        return productGetDto;
    }

    private async Task<Product> _getByIdAsync(int id)
    {
        var product = await _productRepository.GetSingleAsync(x => x.Id == id);
        if (product is null)
            throw new NotFoundException("Product is not found");
        return product;
    }

    public async Task<List<ProductGetDto>> SearchProductsByName(string search)
    {
        var products = await _productRepository.GetFilterAsync(x => x.Name.ToLower().Contains(search.ToLower()), "OrderDetails");
        if (products is null)
            throw new NotFoundException("Product with given name is not found");

        List<ProductGetDto> productsList = new();
        productsList.ForEach(product =>
        {
            ProductGetDto p = new()
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                Description = product.Description,
                OrderDetails = product.OrderDetails
            };
        });

        return productsList;

    }
}
