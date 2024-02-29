using Microsoft.AspNetCore.Mvc;

namespace AppServer.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
