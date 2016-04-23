using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloAspNetCore.Models;
using HelloAspNetCore.Models.Pages;

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
            return new Layout();
        }
    }

    
}
