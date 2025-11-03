using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    public class LoansController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
