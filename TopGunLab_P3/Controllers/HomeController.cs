using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Session;
using TopGunLab_P3.Models;
using TopGunLab_P3.ViewModels;

namespace TopGunLab_P3.Controllers
{
    public class HomeController : Controller
    {
        public static List<Product> _products = new List<Product>();


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_products.ToList());
        }

        public ViewResult Info(string name)
        {
            IEnumerable<Product> list = _products;

            Product product = list.FirstOrDefault(p => p.Name == name);

            return View(product);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProductConfirmed(Product product)
        {
            if(product.Name == null)
            {
                ModelState.AddModelError(string.Empty,"Введите название продукта");
            }
            if(product.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Выбирете кол-во продукта");
            }

            if (ModelState.IsValid)
            {
                _products.Add(product);

                return RedirectToAction("Index");
            }

            return View("AddProduct");
        }

        [HttpGet]
        public RedirectToActionResult DeleteProduct(string name)
        {
            var product = _products.SingleOrDefault(p => p.Name == name);
            _products.Remove(product);

            return RedirectToAction("Index");
        }

        public RedirectToActionResult Edit(string name)
        {
            Product product = _products.SingleOrDefault(p => p.Name == name);
            var model = new ProductViewModel
            {
                Name = product.Name
            };

            return RedirectToAction("EditProduct", model);
        }

        [HttpGet]
        public IActionResult EditProduct(ProductViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public IActionResult EditProductConfirmed(ProductViewModel model)
        {
            if(model.ForCount < 0)
            {
                ModelState.AddModelError(string.Empty, "Количество продуктов не может быть отрицательным");
            }

            if (ModelState.IsValid)
            {
                Product product = _products.SingleOrDefault(p => p.Name == model.Name);

                var newProduct = product;
                newProduct.Unit = model.Unit;

                if (model.IsAdded == true)
                {
                    newProduct.Count += (uint)model.ForCount;
                }
                else
                {
                    newProduct.Count = (uint)model.ForCount;
                }

                _products.Remove(product);
                _products.Add(newProduct);

                return RedirectToAction("Index");
            }

            return RedirectToAction("EditProduct", model);
        }
    }
}
