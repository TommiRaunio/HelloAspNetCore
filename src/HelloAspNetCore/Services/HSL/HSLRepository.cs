using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloAspNetCore.JsonClasses;
using Newtonsoft.Json;

namespace HelloAspNetCore.Services.HSL
{
    public class HSLRepository
    {


        private string CacheKeyFor(LocationEnum from, LocationEnum to)
        {
            return $"{@from.ToString()}-{to.ToString()}";
        }
        
        public async Task<List<HSLRoute>> GetRoute(LocationEnum from, LocationEnum to)
        {

            List<HSLRoute> listOfRoutes = null;
            listOfRoutes = await GetRouteFromHsl(from, to);
            return listOfRoutes;

        }

        private async Task<List<HSLRoute>> GetRouteFromHsl(LocationEnum from, LocationEnum to)
        {
            string routeResponse;
            List<HSLRoute> listOfRoutes;

            using (var hslAdapter = new HslConnector())
            {
                routeResponse = await hslAdapter.GetRoute(HslCoordinateBank.GetCoordinatesFor(from), HslCoordinateBank.GetCoordinatesFor(to));
                listOfRoutes = ParseRouteInformationFromJSON(from, to, routeResponse);

                if (listOfRoutes.Any())
                {
                    var tasks = new List<Task>();

                    listOfRoutes.ForEach(x =>
                    {
                        //Launch all the retriaval tasks right after another...
                        //And await with WhenAll a little down further
                        tasks.Add(SetShortCodesForRoute(x));
                    });

                    //option 2:
                    //listOfRoutes.ForEach(async x => {
                    //    await SetShortCodesForRoute(x);
                    //});

                    //option 3:
                    //Parallel.ForEach(listOfRoutes, async singleRoute =>
                    //    {
                    //        await SetShortCodesForRoute(singleRoute);
                    //    });

                    await Task.WhenAll(tasks);
                }

            }          
          
            return listOfRoutes ?? new List<HSLRoute>();

        }

        private async Task SetShortCodesForRoute(HSLRoute route)
        {

            using (var hslAdapter = new HslConnector())
            {

                if (route.legs.Any())
                {
                    foreach (var leg in route.legs.Where(x => x.code != null))
                    {
                        //the calls to HSL are launched right after another
                        var response = await hslAdapter.GetLine(leg.code);

                        if (!string.IsNullOrEmpty(response))
                        {
                            var fullLineInformation = JsonConvert.DeserializeObject<List<HSLLine>>(response);
                             leg.shortCode = (fullLineInformation.Any()) ? fullLineInformation.First().code_short : "";                           
                        }

                        
                    }
                }

            }
        }

        private List<HSLRoute> ParseRouteInformationFromJSON(LocationEnum from, LocationEnum to, string response){

            List<HSLRoute> listOfRoutes = null;

            try
            {
                if (!string.IsNullOrEmpty(response))
                {

                    var fullReturnedStack = JsonConvert.DeserializeObject<List<List<HSLRoute>>>(response);

                    if (fullReturnedStack.Any()){
                        listOfRoutes = fullReturnedStack.Select(x => x.First()).ToList();
                    }

                }

                return listOfRoutes;

            }
            catch (Exception ex)
            {

                throw new Exception("Kutsu Reittioppaaseen onnistui, mutta vastaus oli odottamatonta formaattia", ex);
            }

        }
    
    } //class
}