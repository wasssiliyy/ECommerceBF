using ECommerce.WebUI.Models;

namespace ECommerce.WebUI.Models
{
    public class ProductAndSortButtonContextViewModel
    {
        public ProductListViewModel Products { get; set; }
        public string NameContext { get; set; }
        public string PriceContext { get; set; }
    }
}