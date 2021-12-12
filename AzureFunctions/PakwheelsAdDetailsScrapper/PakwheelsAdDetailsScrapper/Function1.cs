using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

// packages for scrapping
using CsvHelper;
using HtmlAgilityPack;
using System.Net.Http;
using System.Text.Json;
using System.Text;


namespace PakwheelsAdDetailsScrapper
{
    public static class Function1
    {
        [FunctionName("ScrapDetails")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["adUrl"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            /*string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a adUrl in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";*/

            string responseMessage = string.Empty;

            string url = "https://www.pakwheels.com/used-cars/toyota-hilux-2021-for-sale-in-islamabad-5705506";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(name);

            var carFeatures = new List<string>();
            // scarpping car features
            try
            {
                var featureTags = doc.DocumentNode.SelectSingleNode("//ul[@class='list-unstyled car-feature-list nomargin']");

                try
                {
                    var nestedTags = featureTags.SelectNodes(".//li");
                    foreach (var nestedTag in nestedTags)
                    {
                        carFeatures.Add(nestedTag.InnerText);
                    }
                }
                catch (Exception ex)
                {
                    // do nothing
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
                // do nothing
            }


            // scrapping image urls
            var imageUrls = new List<string>();
            try
            {
                var images = doc.DocumentNode.SelectNodes("//img[@class='lazy-used-car-slider']");
                foreach (var image in images)
                {

                    imageUrls.Add(image.GetAttributeValue("data-original", "no attr"));
                }

            }
            catch (Exception ex)
            {
                // do nothing
            }


            var desc = string.Empty;
            // scrapping the ad details section
            try
            {
                var jsonObjectString = doc.DocumentNode.SelectSingleNode("//div[@class='row ad-listing-template mt10']").SelectSingleNode(".//div[@class='col-md-8']").SelectSingleNode(".//script[@type='application/ld+json']").InnerText;
                var adListingJsonObj = JsonDocument.Parse(jsonObjectString);
                var obj = adListingJsonObj.RootElement;

                desc = obj.GetProperty("description").ToString();

            }
            catch (Exception ex)
            {
                // do nothing
            }

            // appending features list to response message
           /* responseMessage += "=========================================================\n";
            responseMessage += "==================== CAR FEATURES =======================\n";
            foreach (var feature in carFeatures)
            {
                responseMessage += "==  " + feature + "\n";
            }
            responseMessage += "=========================================================\n\n\n";

            // appending image url to response message
            responseMessage += "=========================================================\n";
            responseMessage += "====================== IMAGES URL =======================\n";
            foreach (var image in imageUrls)
            {
                responseMessage += "==  " + image + "\n";
            }
            responseMessage += "=========================================================\n\n\n";

            responseMessage += desc;*/


            /*return new OkObjectResult(responseMessage);*/

            JsonObject responseObject = new JsonObject();
            responseObject.descriptionText = desc;
            responseObject.imageUrls = imageUrls;
            responseObject.carFeatures = carFeatures;

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(responseObject, Formatting.Indented), Encoding.UTF8, "application/json")
                };
        }
    }
}
