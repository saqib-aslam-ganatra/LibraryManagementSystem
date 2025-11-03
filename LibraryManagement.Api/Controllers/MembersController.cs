using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Api.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
