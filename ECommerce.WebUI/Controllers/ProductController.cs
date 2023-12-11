using ECommerce.Business.Abstract;
using ECommerce.DataAccess.Concrete.EFEntityFramework;
using ECommerce.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
        static bool IsOrderByProductName = false;
        static bool IsOrderByProductPrice = false;

        static int pa = 1;
        static int cate = 0;

        static string nameContext = "A-Z";
        static string priceContext = "Lower To Hight";



        public async Task<ActionResult> Index(int page = 1, int category = 0)
        {
            if (pa != 1 || cate != 0)
            {
                page = pa;
                category = cate;

                pa = 1;
                cate = 0;
            }
            var products = await _productService.GetAllByCategory(category);

            int pageSize = 10;

            ProductListViewModel model;


            model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                CurrentCategory = category,
                PageCount = ((int)Math.Ceiling(products.Count / (double)pageSize)),
                PageSize = pageSize,
                CurrentPage = page
            };


            var sortProducts = ProductNameSortOrNotSort(model.Products, IsOrderByProductName);

            var sortProductsPrice = ProductPriceSortOrNotSort(sortProducts, IsOrderByProductPrice);

            model.Products = sortProductsPrice;
            if (!IsOrderByProductPrice && IsOrderByProductName)
            {
                model.Products = sortProducts;
            }
            //if (IsOrderByProductPrice)
            //{
            //}


            ViewBag.SortProductNameButtonContext = nameContext;
            ViewBag.SortProductPriceButtonContext = priceContext;

            //ProductAndSortButtonContextViewModel viewModel = new ProductAndSortButtonContextViewModel
            //{
            //    Products = model,
            //    NameContext = nameContext,
            //    PriceContext = priceContext
            //};

            return View(model);
        }

        public List<Product> ProductNameSortOrNotSort(List<Product> products, bool isSortProductName)
        {
            if (isSortProductName)
            {
                products = products.OrderBy(s => s.ProductName).ToList();
                nameContext = "Z-A";
            }
            else
            {
                products = products.OrderByDescending(s => s.ProductName).ToList();
                nameContext = "A-Z";
            }
            return products;
        }

        public List<Product> ProductPriceSortOrNotSort(List<Product> products, bool isSortProductPrice)
        {
            if (isSortProductPrice)
            {
                products = products.OrderBy(s => s.UnitPrice).ToList();
                priceContext = "Hight To Lower";
            }
            else
            {
                products = products.OrderByDescending(s => s.UnitPrice).ToList();
                priceContext = "Lower To Hight";
            }
            return products;
        }


        //public async Task<ActionResult> Index2()
        //{
        //    List<Product> products;
        //    if (productss.Count > 0)
        //    {
        //        products = productss;
        //    }
        //    else
        //    {
        //        products = await _productService.GetAllByCategory(category);
        //    }

        //    int pageSize = 10;

        //    var model = new ProductListViewModel
        //    {
        //        Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
        //        CurrentCategory = category,
        //        PageCount = ((int)Math.Ceiling(products.Count / (double)pageSize)),
        //        PageSize = pageSize,
        //        CurrentPage = page
        //    };
        //    return View(model);
        //}

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> ProductOrderBy(int pagee = 1, int categoryy = 0, string sort = "")
        {
            IsOrderByProductName = !IsOrderByProductName;
            pa = pagee;
            cate = categoryy;
            //int pageSize = 10;

            //var allProduct = await _productService.GetAllByCategory(category);

            //var data = allProduct.OrderBy(x => x.ProductName).ToList();

            //var viewModel = new ProductListViewModel
            //{
            //    Products = data.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            //    CurrentCategory = category,
            //    PageCount = ((int)Math.Ceiling(data.Count / (double)pageSize)),
            //    PageSize = pageSize,
            //    CurrentPage = page
            //    //Products = data
            //};

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ProductHightToLower(int pagee = 1, int categoryy = 0, string sort = "")
        {
            IsOrderByProductPrice = !IsOrderByProductPrice;
            pa = pagee;
            cate = categoryy;

            return RedirectToAction("Index");
        }
    }


    //public IActionResult ProductOrderByDecending()
    //{

    //}
}
