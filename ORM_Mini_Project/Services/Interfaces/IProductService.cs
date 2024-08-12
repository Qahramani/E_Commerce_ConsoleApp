using ORM_Mini_Project.DTOs.ProductDtos;

namespace ORM_Mini_Project.Services.Interfaces;

public interface IProductService
{
    Task CreateAsync(ProductPostDto productDto);
    Task UpdateAsync(ProductPutDto newProductDto);
    Task DeleteAsync(int id);

    Task<ProductGetDto> GetByIdAsync(int id);
    Task<List<ProductGetDto>> GetAllAsync();
    Task<List<ProductGetDto>> SearchProductsByName(string search);
}
