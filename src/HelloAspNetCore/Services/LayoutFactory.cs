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
                From = LocationBank.Get(LocationEnum.Start),
                To = LocationBank.Get(LocationEnum.Solita)
            });

            layout.Routes.Add(new LocationPair()
            {
                From = LocationBank.Get(LocationEnum.Solita),
                To = LocationBank.Get(LocationEnum.Start)
            });

            layout.Routes.Add(new LocationPair()
            {
                From = LocationBank.Get(LocationEnum.School),
                To = LocationBank.Get(LocationEnum.Solita)
            });

            layout.Routes.Add(new LocationPair()
            {
                From = LocationBank.Get(LocationEnum.Solita),
                To = LocationBank.Get(LocationEnum.School)
            });

            return layout;
        }
    }

    
}
