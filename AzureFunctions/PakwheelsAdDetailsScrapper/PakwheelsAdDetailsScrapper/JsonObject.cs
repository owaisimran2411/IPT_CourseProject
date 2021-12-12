using System;
using System.Collections.Generic;
using System.Text;

namespace PakwheelsAdDetailsScrapper
{
    class JsonObject
    {
        public string descriptionText { get; set; }
        public List<string> imageUrls { get; set; }
        public List<string> carFeatures { get; set; }

        /*public static JsonObject()
        {
            descriptionText = string.Empty;
            imageUrls = new List<string>();
            carFeatures = new List<string>();
        }*/
    }
}
