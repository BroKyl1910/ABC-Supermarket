using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ABCSupermarketMVC.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ABCSupermarketMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ABCSupermarketMVCContext _context;

        public ProductsController(ABCSupermarketMVCContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<string> Create(string ProductName, string ProductDesc, string ProductPrice, IFormFile ProductImage)
        {
            //[Bind("ProductName,ProductDesc,ProductPrice")] Product product
            Product product = new Product()
            {
                ProductName = ProductName,
                ProductDesc = ProductDesc,
                ProductPrice = Convert.ToDecimal(ProductPrice.Replace('.', ','))
            };
            //https://stackoverflow.com/questions/42741170/how-to-save-images-to-database-using-asp-net-core
            using (var ms = new MemoryStream())
            {
                ProductImage.CopyTo(ms);
                product.ProductImage = ms.ToArray();
            }

            _context.Add(product);
            await _context.SaveChangesAsync();
            return "OK";

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<string> Edit(int ProductID, string ProductName, string ProductDesc, string ProductPrice, IFormFile ProductImage)
        {
            Product product = _context.Product.First(p => p.ProductId == ProductID);
            product.ProductName = ProductName;
            product.ProductDesc = ProductDesc;
            product.ProductPrice = Convert.ToDecimal(ProductPrice.Replace('.', ','));

            //If image is null, means value of file select wasnt changed
            if (ProductImage != null)
            {
                using (var ms = new MemoryStream())
                {
                    ProductImage.CopyTo(ms);
                    product.ProductImage = ms.ToArray();
                }
            }

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.ProductId))
                {
                    return "404. Product not found";
                }
                else
                {
                    throw;
                }
            }
            return "OK";
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
