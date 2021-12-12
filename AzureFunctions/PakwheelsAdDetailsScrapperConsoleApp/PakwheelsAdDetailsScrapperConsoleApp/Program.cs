using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Net;

// packages for scrapping
using CsvHelper;
using HtmlAgilityPack;
using System.Net.Http;
using Newtonsoft.Json;

namespace PakwheelsAdDetailsScrapperConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string nameToSend = "https://www.pakwheels.com/used-cars/suzuki-mehran-1997-for-sale-in-toba-tek-singh-5701434";
            string baseURL = "http://localhost:7071/api/ScrapDetails";
            string urlToInvoke = string.Format("{0}?adUrl={1}", baseURL, nameToSend);
            Run(urlToInvoke).Wait();
            
        }

        public static async Task Run(string url)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            string responseString = await response.Content.ReadAsStringAsync();
            dynamic jsonObject = JsonConvert.DeserializeObject(responseString);
            
            Console.WriteLine($"{jsonObject["imageUrls"]}");
            Console.WriteLine($"{jsonObject["carFeatures"]}");
            Console.WriteLine($"{jsonObject["descriptionText"]}");
            
            
        }
    }
}
