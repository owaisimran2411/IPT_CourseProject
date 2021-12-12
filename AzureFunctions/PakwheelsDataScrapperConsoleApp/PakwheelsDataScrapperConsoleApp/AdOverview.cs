using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PakwheelsDataScrapperConsoleApp
{
    class AdOverview
    {
        public string brandName { get; set; }
        public string descriptionText { get; set; }
        public string itemCondition { get; set; }
        public int modelYear { get; set; }
        public string manufacturer { get; set; }
        public string fuelType { get; set; }
        public string imageUrl { get; set; }
        public string transmission { get; set; }
        public int engineDisplacement { get; set; }
        public int mileage { get; set; }
        public long price { get; set; }
        public string detailsUrl { get; set; }
        public int adId { get; set; }

        public void PrintInformation()
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine($"Brand Name: {brandName}");
            Console.WriteLine($"Ad Description: {descriptionText}");
            Console.WriteLine($"Item Condition: {itemCondition}");
            Console.WriteLine($"Model Year: {modelYear}");
            Console.WriteLine($"Manufacturer: {manufacturer}");
            Console.WriteLine($"Fuel Type: {fuelType}");
            Console.WriteLine($"Image Url: {imageUrl}");
            Console.WriteLine($"Transmission Type: {transmission}");
            Console.WriteLine($"Engine Displacement: {engineDisplacement}");
            Console.WriteLine($"Mileage: {mileage}");
            Console.WriteLine($"Price: PKR {price}");
            Console.WriteLine($"Details URL: {detailsUrl}");
            Console.WriteLine($"Ad ID: {adId}");
            Console.WriteLine("===============================================================");
        }

    }
}
