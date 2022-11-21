using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using sqlapp.Models;
using sqlapp.Services;

namespace sqlapp.Pages
{
    public class IndexModel : PageModel
    {

        public bool IsBeta;

        private readonly IProductService _productService;       

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public List<Product> Products; 
        public void OnGet()
        {
            IsBeta = _productService.IsBeta().Result;
            Products= _productService.GetProducts();

        }
    }
}