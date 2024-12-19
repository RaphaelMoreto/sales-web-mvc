using Microsoft.AspNetCore.Mvc;

namespace sales_web_mvc.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
