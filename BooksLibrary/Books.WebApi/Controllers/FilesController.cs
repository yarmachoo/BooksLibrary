using Microsoft.AspNetCore.Mvc;

namespace Books.WebApi.Controllers
{
    public class FilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
