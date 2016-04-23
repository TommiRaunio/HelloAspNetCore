using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloAspNetCore.Models;
using HelloAspNetCore.Models.Pages;
using HelloAspNetCore.Services.HSL;

namespace HelloAspNetCore.Services
{
    public interface ILayoutFactory
    {
        Layout Create();
    }

    public class LayoutFactory : ILayoutFactory
    {
        public Layout Create()
        {
            var layout = new Layout();
            
            layout.Routes.Add(new LocationPair()
            {
                From = LocationBank.Get(LocationEnum.Home),
                To = LocationBank.Get(LocationEnum.TomminWork)
            });

            layout.Routes.Add(new LocationPair()
            {
                From = LocationBank.Get(LocationEnum.TomminWork),
                To = LocationBank.Get(LocationEnum.Home)
            });

            return layout;
        }
    }

    
}
