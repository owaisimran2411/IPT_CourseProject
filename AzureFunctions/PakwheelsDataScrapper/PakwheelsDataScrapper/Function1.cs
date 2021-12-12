using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

using System.Globalization;
using System.Collections.Generic;


// packages for webscrapping
using HtmlAgilityPack;

namespace PakwheelsDataScrapper
{
    public static class Function1
    {
        [FunctionName("DataScrapper")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            /*name = name ?? data?.name;*/




            /*
                Code logic for scrapping pakwheels website
             */

            // creating an object of datastructure
            DataStructureAdOverview dataobject = new DataStructureAdOverview();



            // accessing the website of pakwheels
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlCode = web.Load("https://en.wikipedia.org/wiki/Greece");

            var HeaderNames = htmlCode.DocumentNode.SelectNodes("//span[@class='toctext']");

            name = HeaderNames[1].InnerText;
            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);

            /* string csv = File.ReadAllText("F:\\IPT_CourseProject\\AzureFunctions\\PakwheelsDataScrapperConsoleApp\\PakwheelsDataScrapperConsoleApp\\bin\\Debug\\adsData.csv");
             byte[] fileBytes = Encoding.UTF8.GetBytes(csv);

             return new FileContentResult(fileBytes, "application/octet-stream") { 
                 FileDownloadName = "Export.csv"
             };*/
        }
    }
}
