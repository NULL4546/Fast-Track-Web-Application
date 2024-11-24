using Fast_Track_Web_Application.Models;

namespace Fast_Track_Web_Application.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task<List<Product>> GetAllProductsAsync();

        Task<Product> GetProductByIdAsync(int id);
        Task UpdateProductAsync(Product product);


    }
}
