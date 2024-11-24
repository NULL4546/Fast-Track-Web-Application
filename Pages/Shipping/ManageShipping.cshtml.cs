using Fast_Track_Web_Application.Models;
using Fast_Track_Web_Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fast_Track_Web_Application.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class ManageShippingModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public ManageShippingModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> Products { get; set; }

        [BindProperty]
        public Dictionary<int, string> ShippingStatusUpdates { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _productRepository.GetAllProductsAsync();
            ShippingStatusUpdates = Products.ToDictionary(p => p.Id, p => p.ShippingStatus);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            // Fetch product from the database
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            // Update shipping status
            if (ShippingStatusUpdates.TryGetValue(id, out var newStatus))
            {
                product.ShippingStatus = newStatus;
                await _productRepository.UpdateProductAsync(product);
            }

            return RedirectToPage();
        }
    }
}
