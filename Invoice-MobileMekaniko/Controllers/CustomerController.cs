using Microsoft.AspNetCore.Mvc;

namespace Invoice_MobileMekaniko.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
