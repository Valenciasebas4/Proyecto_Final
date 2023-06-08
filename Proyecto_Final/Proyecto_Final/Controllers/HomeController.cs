﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto_Final.Common;
using Proyecto_Final.DAL;
using Proyecto_Final.DAL.Entities;
using Proyecto_Final.Helpers;
using Proyecto_Final.Models;
using System.Diagnostics;

namespace Proyecto_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBaseContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IOrderHelper _orderHelper;

        public HomeController(ILogger<HomeController> logger, DataBaseContext context, IUserHelper userHelper, IOrderHelper orderHelper)
        {
            _logger = logger;
            _context = context;
            _userHelper = userHelper;
            _orderHelper = orderHelper;
        }


        public async Task<IActionResult> ViewProducts(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "PriceDesc" : "Price";
            ViewBag.UserFullName = GetUserFullName();
            ViewBag.CurrentFilter = searchString;

            IQueryable<Product> query = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => (p.Name.ToLower().Contains(searchString.ToLower()) ||
                                          p.ProductCategories.Any(pc => pc.Category.Name.ToLower()
                                            .Contains(searchString.ToLower()))));
            }

            //List<Product>? products = await _context.Products
            //   .Include(p => p.ProductImages)
            //   .Include(p => p.ProductCategories)
            //   .OrderBy(p => p.Description)
            //   .ToListAsync();

            switch (sortOrder)
            {
                case "NameDesc":
                    query = query.OrderByDescending(p => p.Name);
                    break;
                case "Price":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "PriceDesc":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                default:
                    query = query.OrderBy(p => p.Name);
                    break;
            }

            //Begins New change
            HomeViewModel homeViewModel = new()
            {
                Products = await query.ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };

            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user != null)
            {
                homeViewModel.Quantity = await _context.TemporalSales
                    .Where(ts => ts.User.Id == user.Id)
                    .SumAsync(ts => ts.Quantity);
            }

            return View(homeViewModel);
            //Ends New change
        }


        
         public async Task<IActionResult> Index()
        {
            ViewBag.UserFullName = GetUserFullName();

            return View();
        }
        
        /*
                public async Task<IActionResult> ViewProducts()
                {
                    List<Product>? products = await _context.Products
                         .Include(p => p.ProductImages)
                         .Include(p => p.ProductCategories)
                         .OrderBy(p => p.Description)
                         .ToListAsync();

                    //Variables de Sesión
                    ViewBag.UserFullName = GetUserFullName();

                    HomeViewModel homeViewModel = new()
                    {
                        Products = products
                    };

                    User user = await _userHelper.GetUserAsync(User.Identity.Name);
                    if (user != null)
                    {
                        homeViewModel.Quantity = await _context.TemporalSales
                            .Where(ts => ts.User.Id == user.Id)
                            .SumAsync(ts => ts.Quantity);
                    }

                    return View(homeViewModel);
                }
        */


        private string GetUserFullName()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.FullName)
                .FirstOrDefault();
        }




        public IActionResult Privacy()
        {
            ViewBag.UserFullName = GetUserFullName();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            ViewBag.UserFullName = GetUserFullName();
            return View();
        }

        public async Task<IActionResult> AddProductInCart(Guid? productId)
        {
            ViewBag.UserFullName = GetUserFullName();
            if (productId == null) return NotFound();

            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            Product product = await _context.Products.FindAsync(productId);
            User user = await _userHelper.GetUserAsync(User.Identity.Name);

            if (user == null || product == null) return NotFound();

            // Busca una entrada existente en la tabla TemporalSale para este producto y usuario
            TemporalSale existingTemporalSale = await _context.TemporalSales
                .Where(t => t.Product.Id == productId && t.User.Id == user.Id)
                .FirstOrDefaultAsync();

            if (existingTemporalSale != null)
            {
                // Si existe una entrada, incrementa la cantidad
                existingTemporalSale.Quantity += 1;
                existingTemporalSale.ModifiedDate = DateTime.Now;
            }
            else
            {
                // Si no existe una entrada, crea una nueva
                TemporalSale temporalSale = new()
                {
                    CreatedDate = DateTime.Now,
                    Product = product,
                    Quantity = 1,
                    User = user
                };

                _context.TemporalSales.Add(temporalSale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ViewProducts));
        }

        public async Task<IActionResult> DetailsProduct(Guid? productId)
        {
            ViewBag.UserFullName = GetUserFullName();
            if (productId == null) return NotFound();

            Product product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null) return NotFound();

            string categories = string.Empty;

            foreach (ProductCategory? category in product.ProductCategories)
                categories += $"{category.Category.Name}, ";

            categories = categories.Substring(0, categories.Length - 2);

            DetailsProductToCartViewModel detailsProductToCartViewModel = new()
            {
                Categories = categories,
                Description = product.Description,
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                ProductImages = product.ProductImages,
                Quantity = 1,
                Stock = product.Stock,
            };

            return View(detailsProductToCartViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsProduct(DetailsProductToCartViewModel detailsProductToCartViewModel)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");

            Product product = await _context.Products.FindAsync(detailsProductToCartViewModel.Id);
            User user = await _userHelper.GetUserAsync(User.Identity.Name);

            if (product == null || user == null) return NotFound();

            // Busca una entrada existente en la tabla TemporalSale para este producto y usuario
            TemporalSale existingTemporalSale = await _context.TemporalSales
                .Where(t => t.Product.Id == detailsProductToCartViewModel.Id && t.User.Id == user.Id)
                .FirstOrDefaultAsync();

            if (existingTemporalSale != null)
            {
                // Si existe una entrada, incrementa la cantidad
                existingTemporalSale.Quantity += detailsProductToCartViewModel.Quantity;
                existingTemporalSale.ModifiedDate = DateTime.Now;
            }
            else
            {
                // Si no existe una entrada, crea una nueva
                TemporalSale temporalSale = new()
                {
                    Product = product,
                    Quantity = 1,
                    User = user,
                    Remarks = detailsProductToCartViewModel.Remarks,
                };

                _context.TemporalSales.Add(temporalSale);
            }

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(ViewProducts));
        }

        [Authorize] //Etiqueta para que solo usuarios logueados puedan acceder a este método.
        public async Task<IActionResult> ShowCartAndConfirm()
        {
            ViewBag.UserFullName = GetUserFullName();
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null) return NotFound();

            List<TemporalSale>? temporalSales = await _context.TemporalSales
                .Include(ts => ts.Product)
                .ThenInclude(p => p.ProductImages)
                .Where(ts => ts.User.Id == user.Id)
                .ToListAsync();

            ShowCartViewModel showCartViewModel = new()
            {
                User = user,
                TemporalSales = temporalSales
            };

            return View(showCartViewModel);
        }

        

        public async Task<IActionResult> DecreaseQuantity(Guid? temporalSaleId)
        {
            ViewBag.UserFullName = GetUserFullName();
            if (temporalSaleId == null) return NotFound();

            TemporalSale temporalSale = await _context.TemporalSales.FindAsync(temporalSaleId);
            if (temporalSale == null) return NotFound();

            if (temporalSale.Quantity > 1)
            {
                temporalSale.Quantity--;
                temporalSale.ModifiedDate = DateTime.Now;
                _context.TemporalSales.Update(temporalSale);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(ShowCartAndConfirm));
        }

        public async Task<IActionResult> IncreaseQuantity(Guid? temporalSaleId)
        {
            ViewBag.UserFullName = GetUserFullName();
            if (temporalSaleId == null) return NotFound();

            TemporalSale temporalSale = await _context.TemporalSales.FindAsync(temporalSaleId);
            if (temporalSale == null) return NotFound();

            temporalSale.Quantity++;
            temporalSale.ModifiedDate = DateTime.Now;
            _context.TemporalSales.Update(temporalSale);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ShowCartAndConfirm));
        }

        public async Task<IActionResult> DeleteTemporalSale(Guid? temporalSaleId)
        {
            ViewBag.UserFullName = GetUserFullName();
            if (temporalSaleId == null) return NotFound();

            TemporalSale temporalSale = await _context.TemporalSales.FindAsync(temporalSaleId);
            if (temporalSale == null) return NotFound();

            _context.TemporalSales.Remove(temporalSale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShowCartAndConfirm));
        }

        public async Task<IActionResult> EditTemporalSale(Guid? temporalSaleId)
        {
            ViewBag.UserFullName = GetUserFullName();
            if (temporalSaleId == null) return NotFound();

            TemporalSale temporalSale = await _context.TemporalSales.FindAsync(temporalSaleId);
            if (temporalSale == null) return NotFound();

            EditTemporalSaleViewModel editTemporalSaleViewModel = new()
            {
                Id = temporalSale.Id,
                Quantity = temporalSale.Quantity,
                Remarks = temporalSale.Remarks,
                ModifiedDate = DateTime.Now,
            };

            return View(editTemporalSaleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTemporalSale(Guid? temporalSaleId, EditTemporalSaleViewModel editTemporalSaleViewModel)
        {
            if (temporalSaleId != editTemporalSaleViewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    TemporalSale temporalSale = await _context.TemporalSales.FindAsync(temporalSaleId);
                    temporalSale.Quantity = editTemporalSaleViewModel.Quantity;
                    temporalSale.Remarks = editTemporalSaleViewModel.Remarks;
                    temporalSale.ModifiedDate = DateTime.Now;
                    _context.Update(temporalSale);
                    await _context.SaveChangesAsync();
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                    return View(editTemporalSaleViewModel);
                }

                return RedirectToAction(nameof(ShowCartAndConfirm));
            }

            return View(editTemporalSaleViewModel);
        }

        [Authorize]
        public IActionResult OrderSuccess()
        {
            ViewBag.UserFullName = GetUserFullName();
            //string serializedData = TempData["showCartViewModel"] as string;
            //ShowCartViewModel showCartViewModel = JsonConvert.DeserializeObject<ShowCartViewModel>(serializedData);

            // Realiza las acciones necesarias con el objeto showCartViewModel

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowCartAndConfirm(ShowCartViewModel showCartViewModel)
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null) return NotFound();

            showCartViewModel.User = user;
            showCartViewModel.TemporalSales = await _context.TemporalSales
                .Include(ts => ts.Product)
                .ThenInclude(p => p.ProductImages)
                .Where(ts => ts.User.Id == user.Id)
            .ToListAsync();

            Response response = await _orderHelper.ProcessOrderAsync(showCartViewModel);
            if (response.IsSuccess) return RedirectToAction(nameof(OrderSuccess));

            ModelState.AddModelError(string.Empty, response.Message);
            return View(showCartViewModel);
        }


        // Eliminar todos los productos del carrito
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null) return NotFound();

            List<TemporalSale> temporalSale = await _context.TemporalSales
                .Where(ts => ts.User.Id == user.Id)
                .ToListAsync();

            _context.RemoveRange(temporalSale);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ViewProducts));
        }




        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowCartAndConfirm(ShowCartViewModel showCartViewModel)
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null) return NotFound();

            showCartViewModel.User = user;
            showCartViewModel.TemporalSales = await _context.TemporalSales
                .Include(ts => ts.Product)
                .ThenInclude(p => p.ProductImages)
                .Where(ts => ts.User.Id == user.Id)
                .ToListAsync();

            Response response = await _orderHelper.ProcessOrderAsync(showCartViewModel);
            if (response.IsSuccess)
            {
                if (response.IsSuccess)
                {
                    string serializedData = JsonConvert.SerializeObject(showCartViewModel);
                    TempData["showCartViewModel"] = serializedData;
                    return RedirectToAction(nameof(OrderSuccess));
                }
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return RedirectToAction(nameof(OrderSuccess), new { showCartViewModel });//
        }
        */

        /*
        [Authorize]
        public IActionResult OrderSuccess(ShowCartViewModel showCartViewModel)
        {
            //var showCartViewModel = TempData["showCartViewModel"] as ShowCartViewModel;

            //if (showCartViewModel == null) return RedirectToAction(nameof(ShowCartAndConfirm));

            OrderSuccessViewModel orderSuccessViewModel = new()
            {
                Quantity = showCartViewModel.Quantity,
                Price = showCartViewModel.Value
            };
            return View(orderSuccessViewModel);
        }
        */
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShowCartAndConfirm(ShowCartViewModel showCartViewModel)
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            if (user == null) return NotFound();

            showCartViewModel.User = user;
            showCartViewModel.TemporalSales = await _context.TemporalSales
                .Include(ts => ts.Product)
                .ThenInclude(p => p.ProductImages)
                .Where(ts => ts.User.Id == user.Id)
            .ToListAsync();

            Response response = await _orderHelper.ProcessOrderAsync(showCartViewModel);
            if (response.IsSuccess)
            {
                OrderSuccess(showCartViewModel);
                TempData["showCartViewModel"] = showCartViewModel;
                return RedirectToAction(nameof(OrderSuccess));
            }

            ModelState.AddModelError(string.Empty, response.Message);
            return View(showCartViewModel);
        }
        */
    }
}