using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloAspNetCore.Models.Pages;
using HelloAspNetCore.Services;
using HelloAspNetCore.Services.HSL;
using Microsoft.AspNet.Mvc;


namespace HelloAspNetCore.Controllers
{
    [Route("HaeReitti")]
    public class RouteResultController : Controller
    {

        private readonly IHslRouteSolver _hslRouteSolver;
        private readonly ILayoutFactory _layoutFactory;

        public RouteResultController(IHslRouteSolver hslRouteSolver, ILayoutFactory layoutFactory)
        {
            _hslRouteSolver = hslRouteSolver;
            _layoutFactory = layoutFactory;
        }

        public async Task<IActionResult> Index(LocationEnum from, LocationEnum to)
        {
            //var route = await _hslRouteSolver.GetRoute(from, to);
            var resultPage = new RouteResultPage
            {
                Layout = _layoutFactory.Create()
            };
            return View("~/Views/Pages/RouteResult.cshtml", resultPage);
        }
    }
}
