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
        public async Task<IActionResult> Index(string q)
        {
            //q used to search
            if (q == null)
            {
                return View(await _context.Product.OrderBy(p=> p.ProductName).ToListAsync());
            }
            return View(await _context.Product.Where(p=>p.ProductName.Contains(q) || p.ProductDesc.Contains(q)).OrderBy(p => p.ProductName).ToListAsync());
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> Create(string ProductName, string ProductDesc, string ProductPrice, IFormFile ProductImage)
        {
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        /*Separate validation method so that I can perform backend validation on create and edit forms as I am not using razor, I'm using XMLHttpRequest
          so I need to validate manually*/
        public string Validate([Bind("ProductID,ProductName,ProductDesc,ProductPrice")] Product product, IFormFile ProductImage, bool Editing)
        {
            //https://stackoverflow.com/questions/42741170/how-to-save-images-to-database-using-asp-net-core
            if (ProductImage != null)
            {
                using (var ms = new MemoryStream())
                {
                    ProductImage.CopyTo(ms);
                    product.ProductImage = ms.ToArray();
                }
            }

            //Manual checking because cannot bind IFormFile directly to Product object
            /*ProductImage can be null because on loading the edit page, the image display is set but the image input isn't,
             therefore when editing, the product can still have a stored image even if one isn't posted with the form*/
            if (product.ProductImage == null && !Editing)
            {
                return "Please provide an image";
            }

            string[] acceptedImageFileTypes = new string[] {"png", "jpg","jpeg","gif" };

            if(ProductImage!=null && !acceptedImageFileTypes.Contains(ProductImage.FileName.Split('.')[1]))
            {
                return "Please provide an image of type .png, .jpg, or .gif";
            }

            if (ModelState.IsValid)
            {
                return "OK";
            }

            return ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)).ToList()[0];
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
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
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
