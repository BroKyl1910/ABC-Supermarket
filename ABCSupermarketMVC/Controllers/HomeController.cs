using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ABCSupermarketMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        public IActionResult Index()
        {
            //I don't want a home page, so as soon as the page loads, redirect to the products page
            return RedirectToAction("Index", "Products");
        }
    }
}
