using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Text.Json;

// packages for scrapping
using CsvHelper;
using HtmlAgilityPack;
using System.Net.Http;
using Newtonsoft.Json;


namespace PakwheelsDataScrapperConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // creating an HTML web object and accessing Pakwheels
            var url = "http://localhost:7070/api/DataScrapper/";
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetStringAsync(url);
            
            Console.WriteLine($"Response: {res.ToString()}");

            /*HtmlWeb web = new HtmlWeb();*/

            /*var adsArray = new List<AdOverview>();

            // looping 100 times
            for(int i=0; i<20; i++)
            {
                string url = "https://www.pakwheels.com/used-cars/search/";
                url += (i == 0) ? "-/" : "?page=" + (i + 1).ToString(); ;
                HtmlDocument doc = web.Load(url);

                var AdsSections = doc.DocumentNode.SelectNodes("//ul[@class='list-unstyled search-results search-results-mid next-prev car-search-results ']");


                foreach(var adSection in AdsSections)
                {
                    *//*Console.WriteLine($"{AdsSections[0]}");*//*
                    var AdsListing = adSection.SelectNodes(".//li");

                    foreach(var adListing in AdsListing)
                    {
                        *//*Console.WriteLine($"{adListing.HasAttributes}");*//*
                        if (adListing.HasAttributes)
                        {
                            
                            Console.WriteLine("Parsing Starts");
                            *//*Console.WriteLine($"{adListing.Attributes["id"].Value}");*//*
                            var AdsListingJsonString = adListing.SelectNodes(".//script[@type='application/ld+json']");
                            try
                            {
                                Console.WriteLine($"{AdsListingJsonString[0].InnerText}");
                                var AdsListingJsonObject = JsonDocument.Parse(AdsListingJsonString[0].InnerText);
                                Console.WriteLine("Parsing done");


                                AdOverview adObject = new AdOverview();
                                var obj = AdsListingJsonObject.RootElement;

                                // getting the necessary information

                                var brandInformation = obj.GetProperty("brand");
                                var engineInformation = obj.GetProperty("vehicleEngine");
                                var priceInformation = obj.GetProperty("offers");

                                var adIDString = priceInformation.GetProperty("url").ToString().Split('-');
                                int adID = Convert.ToInt32(adIDString[adIDString.Length - 1]);

                                adObject.brandName = (brandInformation.GetProperty("name")).ToString();
                                adObject.descriptionText = obj.GetProperty("description").ToString();
                                adObject.itemCondition = obj.GetProperty("itemCondition").ToString();
                                adObject.modelYear = Convert.ToInt32(obj.GetProperty("modelDate").ToString());
                                adObject.manufacturer = obj.GetProperty("manufacturer").ToString();
                                adObject.fuelType = obj.GetProperty("fuelType").ToString();
                                adObject.imageUrl = obj.GetProperty("image").ToString();
                                adObject.transmission = obj.GetProperty("vehicleTransmission").ToString();
                                adObject.engineDisplacement = Convert.ToInt32(engineInformation.GetProperty("engineDisplacement").ToString().Substring(0, engineInformation.GetProperty("engineDisplacement").ToString().Length - 2));
                                *//*Console.WriteLine($"Mileage: {(obj.GetProperty("mileageFromOdometer").ToString().Split(' ')[0]).Replace(",", "")}");*//*
                                adObject.mileage = Convert.ToInt32((obj.GetProperty("mileageFromOdometer").ToString().Split(' ')[0]).Replace(",", ""));
                                adObject.price = Convert.ToInt32(priceInformation.GetProperty("price").ToString());
                                adObject.detailsUrl = priceInformation.GetProperty("url").ToString();
                                adObject.adId = adID;


                                *//*Console.WriteLine($"============================\nUrl: {url}, AdsCount: {obj}\n==============================\n\n");*/
            /*adObject.PrintInformation();*//*
            adsArray.Add(adObject);
        }
        catch(Exception ex)
        {
            // do nothing
        }

    }
}
}



}

using (var writer = new StreamWriter("adsData.csv"))
using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
{
csv.WriteRecords(adsArray);
}*/
        }
    }
}
