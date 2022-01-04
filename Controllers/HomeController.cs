using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductsyCategories.Models;

using Microsoft.EntityFrameworkCore;


namespace ProductsyCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }

        //index shows all categories
        [HttpGet ("")]
        public IActionResult Index()
        {
            ViewBag.allCategories = _context.categoriesT.ToList();
            return View();
        }

        // show all products
        [HttpGet("AllProducts")]
        public IActionResult AllProducts()
            {
                ViewBag.allProducts = _context.productsT.ToList();
                return View();
            }

        //Go to selected category
        [HttpGet("category/{categoryId}")]
        public IActionResult SingleCategory(int categoryId )
            {
                // get the specific category that will be displayed on of page using its ID
                Category RetrievedCategory = _context.categoriesT
                .SingleOrDefault(categoria => categoria.CategoryId == categoryId);
                ViewBag.SingleCategory = RetrievedCategory;

                // get all products in this category
                ViewBag.ProductsInCategory = _context.productsT.Include(p => p.ProductsCategories)
                    .Where(p => p.ProductsCategories.Any(p => p.CategoryId == categoryId))
                    .ToList();
                   
                
                // gets the rest of the products that aren't in the category yet so user can add any if needed
                ViewBag.productsUnrelated = _context.productsT //go in products table
                    .Include(p => p.ProductsCategories) //include the list of products in that category
                    .Where(p => p.ProductsCategories.All(p => p.CategoryId != categoryId)); //where each product's category ID doesnt match the one selected
                return View();
            }

        // Go to selected product
        [HttpGet("product/{productId}")]
        public IActionResult SingleProduct(int productId)
            {
                // Get the selected product 
                Product RetrievedProduct = _context.productsT
                    .SingleOrDefault(product => product.ProductId == productId);
                ViewBag.SingleProduct = RetrievedProduct;

                // get all categories this product belongs to 
                ViewBag.CategoriesFromProduct = _context.categoriesT
                    .Include(c=> c.CategoriesProducts)
                    .Where(c => c.CategoriesProducts.Any(c => c.ProductId == productId))
                    .ToList(); 
                
                // get the categories this product does not belong to yet 
                ViewBag.categoriesUnrelated = _context.categoriesT
                    .Include(c => c.CategoriesProducts)
                    // All the CategoriesProducts that are == to this Products ID
                    .Where(c => c.CategoriesProducts.All(c => c.ProductId != productId));

                return View();
            }

        // Add category in general
        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Category newCategory)
            {
                _context.Add(newCategory);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        
        // Add product in general
        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product newProduct)
            {
                _context.Add(newProduct);
                _context.SaveChanges();
                return RedirectToAction("AllProducts");
            }
        
        // Adding a product to a category
        [HttpPost("category/{categoryId}")]
        public IActionResult AddProductToCategory(Association newProductInCategory)
            {
                _context.associationsT.Add(newProductInCategory);
                _context.SaveChanges();
                return RedirectToAction("SingleCategory");
            }

        // Adding a category to a product
        [HttpPost("product/{productId}")]
        public IActionResult AddCategoryToProduct(Association newCategoryInProduct)
            {
                _context.associationsT.Add(newCategoryInProduct);
                _context.SaveChanges();
                return RedirectToAction("SingleProduct");
            }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
