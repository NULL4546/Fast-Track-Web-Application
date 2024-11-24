using Fast_Track_Web_Application.Models;
using Fast_Track_Web_Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Fast_Track_Web_Application.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<AddProductModel> _logger;

        public AddProductModel(IProductRepository productRepository, ILogger<AddProductModel> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Log the errors in ModelState
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError("Model state error: {ErrorMessage}", error.ErrorMessage);
                }

                // If the model state is invalid, return to the page to show validation errors
                return Page();
            }

            try
            {
                // Log the product data for debugging purposes
                _logger.LogInformation("Adding product: {ProductName}, {ProductDetails}", Product.Name, Product.Details);

                // Add the product to the database
                await _productRepository.AddProductAsync(Product);

                // Redirect to the Product List page after adding the product
                return RedirectToPage("ProductList");
            }
            catch (Exception ex)
            {
                // Log any errors during the process of adding the product
                _logger.LogError(ex, "An error occurred while adding the product.");
                return Page();
            }

            // Ensure ShippingStatus is set if not provided
            if (string.IsNullOrEmpty(Product.ShippingStatus))
            {
                Product.ShippingStatus = "Pending"; // Default status
            }

            _logger.LogInformation($"Adding product: {Product.Name}, {Product.Details}");
            await _productRepository.AddProductAsync(Product);
            return RedirectToPage("ProductList");
        }
    }
}
