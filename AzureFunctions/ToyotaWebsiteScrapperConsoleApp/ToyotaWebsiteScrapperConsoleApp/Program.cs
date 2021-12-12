using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

using HtmlAgilityPack;

namespace ToyotaWebsiteScrapperConsoleApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();

            string webUrl = "https://www.toyota-indus.com/";

            HtmlDocument doc = web.Load(webUrl);

            try
            {
                var DataRow = doc.DocumentNode.SelectSingleNode("//div[@class='row discover-range-cont']");
                

                try
                {
                    var a = DataRow.SelectSingleNode(".//div[@class='container']");
                    var b = a.SelectSingleNode(".//div[@class='eds-animate edsanimate-sis-hidden ']");
                    var c = b.SelectSingleNode(".//div[@id='lcs_logo_carousel_slider']");
                    var d = c.SelectNodes(".//div[@class='lcs_logo_container']");
                    /*Console.WriteLine($"Images Count: {d.Count}");*/
                    int i = 0;
                    foreach(var alpha in d)
                    {
                        // searching redirecting URL tag
                        var redirectUrl = alpha.SelectSingleNode(".//a");
                        /*Console.WriteLine($"{redirectUrl.GetAttributeValue("href","")}");*/
                        
                        
                        string url = string.Empty;      // variable to store redirect url
                        string price = string.Empty;    // variable to store price of car
                        string name = string.Empty;     // variable to store the name of the car
                        string image = string.Empty;    // variable to store the image url of the car
                        string slogan = string.Empty;   // variable to store the slogan of the car

                        if(redirectUrl.GetAttributeValue("href", "").Contains("https://toyota-indus.com"))
                        {
                            url = redirectUrl.GetAttributeValue("href", "");
                        }
                        else
                        {
                            url = "https://toyota-indus.com/" + redirectUrl.GetAttributeValue("href", "");
                        }

                        // searching for image tag
                        var imageUrl = redirectUrl.SelectSingleNode(".//img");
                        image = imageUrl.GetAttributeValue("src", "");
                        /*Console.WriteLine($"{imageUrl.GetAttributeValue("src", "")}");*/

                        var e = alpha.SelectSingleNode(".//a[@target='_blank']");

                        var f = e.SelectSingleNode(".//div[@class='lcs_logo_title']");
                        string details = f.InnerText;

                        
                        /*Console.WriteLine(details);*/
                        i++;
                        var information = details.Split(new string[] { "  " }, StringSplitOptions.None);

                        // splitting the information for necessary section
                        name = information[0];

                        /*Console.WriteLine($"{details}");*/
                        Console.WriteLine($"Name: {name}");

                        var g = f.SelectSingleNode(".//span");
                        Console.WriteLine($"Price: {g.InnerText}");

                        var h = f.SelectSingleNode(".//div[@class='slogan']");
                        Console.WriteLine($"Slogan: {h.InnerText}\n");





                        /*Console.WriteLine("Tag Found");*/
                    }
                    /*Console.WriteLine($"AdsCount: {i}");*/

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Found while looking for carousel tag");
                }
            }
            catch(Exception ex)
            {
                // do nothing
            }

        }
    }
}
