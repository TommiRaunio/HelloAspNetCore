using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.PlatformAbstractions;

namespace HelloAspNetCore.Services.HSL
{
    public class HslConnector : HttpClient
    {
        private string _password;
        private string _userId;
        private string _baseAddress = "http://api.reittiopas.fi/hsl/prod/";
        private readonly IApplicationEnvironment _appEnvironment;

        public HslConnector(IApplicationEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            BaseAddress = new Uri(_baseAddress);
            DefaultRequestHeaders.Accept.Clear();
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ReadUserIds();
        }

        private void ReadUserIds()
        {
            try
            {
                var fileContent =
                    System.IO.File.ReadLines(Path.Combine(_appEnvironment.ApplicationBasePath, "Identity.txt"));
                var splitContent = fileContent.First().Split(',');
                _userId = splitContent.First();
                _password = splitContent.Last();

            }
            catch (Exception)
            {
                throw new Exception("Identifikaatiotiedon alustus epäonnistui");

            }
        }

        private string GetIdentityStringForGET()
        {
            return string.Format("user={0}&pass={1}", _userId, _password);
        }

        private string GetFormatForGET()
        {
            return "format=json";
        }

        /// <summary>
        /// Example: api.reittiopas.fi/hsl/prod/?request=route&user=<user>&pass=<pass>&format=txt&from=2548196,6678528&to=2549062,6678638
        /// </summary>
        /// <param name="fromCoordinates"></param>
        /// <param name="toCoordinates"></param>
        /// <returns></returns>
        public async Task<String> GetRoute(string fromCoordinates, string toCoordinates)
        {

            var requestUrl = $"?request=route&{GetFormatForGET()}&{GetIdentityStringForGET()}&from={fromCoordinates}&to={toCoordinates}";
            return await CallHsl(requestUrl);

        }

        /// <summary>
        /// Example: http://api.reittiopas.fi/hsl/prod/?request=lines&user=<id>&pass=<pw>&format=txt&query=2052%20%201|Tapiola
        /// </summary>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public async Task<String> GetLine(string lineCode)
        {
            var requestUrl = $"?request=lines&{GetIdentityStringForGET()}&{GetFormatForGET()}&query={lineCode}";
            return await CallHsl(requestUrl);

        }


        private async Task<string> CallHsl(string requestUrl)
        {
            var response = await GetAsync(requestUrl);
            return response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : null;
        }
    }
} 