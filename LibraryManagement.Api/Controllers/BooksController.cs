using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
