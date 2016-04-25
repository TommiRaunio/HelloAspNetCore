using System.Threading.Tasks;
using HelloAspNetCore.Models.Pages;
using HelloAspNetCore.Services;
using HelloAspNetCore.Services.HSL;
using Microsoft.AspNet.Mvc;

namespace HelloAspNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHslRouteSolver _hslRouteSolver;
        private readonly ILayoutFactory _layoutFactory;

        public HomeController(IHslRouteSolver hslRouteSolver, ILayoutFactory layoutFactory)
        {
            _hslRouteSolver = hslRouteSolver;
            _layoutFactory = layoutFactory;
        }

        public async Task<IActionResult> Index()
        {
            var homePage = new HomePage();
            homePage.Layout = _layoutFactory.Create();

            return View("~/Views/Pages/Home.cshtml", homePage);
        }
    }
}
