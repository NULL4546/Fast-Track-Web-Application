using Fast_Track_Web_Application.Data;
using Fast_Track_Web_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Fast_Track_Web_Application.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Corrected method signature to align with the interface
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            try
            {
                product.TrackingNumber = GenerateTrackingNumber(); // Add tracking number
                _context.Products.Add(product); // Add product to DbContext
                await _context.SaveChangesAsync(); // Commit changes to database
            }
            catch (Exception ex)
            {
                // Log any errors here
                throw new Exception("Error saving product to database", ex);
            }
        }


        public async Task<Product> GetProductByTrackingNumberAsync(string trackingNumber)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.TrackingNumber == trackingNumber);
        }

        private string GenerateTrackingNumber()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

    }
}
