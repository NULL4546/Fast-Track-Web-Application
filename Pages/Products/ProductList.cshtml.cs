using Fast_Track_Web_Application.Models;
using Fast_Track_Web_Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fast_Track_Web_Application.Pages.Products
{
    [Authorize(Roles = "Admin")]
    public class ProductListModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public ProductListModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            Products = await _productRepository.GetAllProductsAsync();
        }
    }

}
