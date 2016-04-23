using System.Collections.Generic;
using System.Linq;
using HelloAspNetCore.JsonClasses;
using HelloAspNetCore.Models;

namespace HelloAspNetCore.Services.HSL
{
    public class RouteBank
    {
        private static Dictionary<LocationEnum, Location> _locations;

        private static Dictionary<LocationEnum, Location> Locations => _locations ?? (_locations = new Dictionary<LocationEnum, Location>());

        public static void Add(LocationEnum location, string friendlyName)
        {
            Locations.Add(location, new Location()
            {
                FriendlyName = friendlyName,
                LocationId = location,
            });
        }

        public static List<Location> GetAll()
        {
            return Locations.Values.ToList();
        }
    }
}