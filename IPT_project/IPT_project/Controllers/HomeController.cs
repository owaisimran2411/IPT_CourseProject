using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IPT_project.Models;
using System.Web.Script.Serialization; 
using System.IO;
using System.Configuration;

namespace IPT_project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            string Json = System.IO.File.ReadAllText("F:\\IPT_CourseProject\\AhsanGIT\\IPT_project\\PythonScrapping\\data.json");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var carlist = ser.Deserialize<List<CarDetail.Rootobject>>(Json);
            

            return View(carlist);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Detail(string carID)
        {
            ViewBag.id = carID;

            return View();
        }
    }
}