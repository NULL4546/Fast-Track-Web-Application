using Fast_Track_Web_Application.Models;
using Fast_Track_Web_Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fast_Track_Web_Application.Pages.Shipping
{
    [Authorize(Roles = "Admin")]
    public class EditShippingStatusModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public EditShippingStatusModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productRepository.GetProductByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _productRepository.UpdateProductAsync(Product);
            return RedirectToPage("ManageShipping");
        }
    }
}
