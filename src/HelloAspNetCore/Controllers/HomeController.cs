using System.Threading.Tasks;
using HelloAspNetCore.Services.HSL;
using Microsoft.AspNet.Mvc;

namespace HelloAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHslRouteSolver _hslRouteSolver;

        public HomeController(IHslRouteSolver hslRouteSolver)
        {
            _hslRouteSolver = hslRouteSolver;
        }

        public async Task<IActionResult> Index()
        {
            //var result = await _hslRouteSolver.GetRoute(LocationEnum.Home, LocationEnum.TomminWork);
            return View("~/Views/Pages/Home.cshtml");
        }
    }
}
