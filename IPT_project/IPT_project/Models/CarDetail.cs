using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IPT_project.Models
{
    public class CarDetail
    {

        public class Rootobject
        {
            public string context { get; set; }
            public string type { get; set; }
            public Brand brand { get; set; }
            public string description { get; set; }
            public string itemCondition { get; set; }
            public int modelDate { get; set; }
            public string manufacturer { get; set; }
            public string fuelType { get; set; }
            public string name { get; set; }
            public string image { get; set; }
            public string vehicleTransmission { get; set; }
            public Vehicleengine vehicleEngine { get; set; }
            public string mileageFromOdometer { get; set; }
            public Offers offers { get; set; }
        }

        public class Brand
        {
            public string type { get; set; }
            public string name { get; set; }
        }

        public class Vehicleengine
        {
            public string type { get; set; }
            public string engineDisplacement { get; set; }
        }

        public class Offers
        {
            public string context { get; set; }
            public string type { get; set; }
            public int price { get; set; }
            public string availability { get; set; }
            public string priceCurrency { get; set; }
            public string url { get; set; }
        }

    }
}