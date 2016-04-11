using System.Collections.Generic;
using System.Linq;
using HelloAspNetCore.JsonClasses;

namespace HelloAspNetCore.Services.HSL
{
    public class RouteBank
    {
        private static Dictionary<LocationEnum, RouteOrigin> _locations;

        private static Dictionary<LocationEnum, RouteOrigin> Locations
        {
            get { return _locations ?? (_locations = new Dictionary<LocationEnum, RouteOrigin>()); }
        }

        public static void Add(LocationEnum location, string friendlyName)
        {
            Locations.Add(location, new RouteOrigin()
            {
                FriendlyName = friendlyName,
                Location = location,
            });
        }

        public static List<RouteOrigin> GetAll()
        {
            return Locations.Values.ToList();
        }
    }
}