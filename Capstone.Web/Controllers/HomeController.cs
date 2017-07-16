using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";

        // GET: Home
        public ActionResult Index()
        {
            ParkSqlDAL parkDAL = new ParkSqlDAL(connectionString);
            List<Park> parkList = parkDAL.GetParks();
            return View("Index", parkList);
        }

        public ActionResult ParkDetail(string id)
        {
            ParkSqlDAL parkDAL = new ParkSqlDAL(connectionString);
            Park thisPark = parkDAL.GetSpecificPark(id);

            if (Session["TemperatureMeasure"] != null)
            {
                thisPark.IsFarenheit = (bool)Session["TemperatureMeasure"];
            }
            else
            {
                Session["TemperatureMeasure"] = true;
            }

            return View("ParkDetail", thisPark);
        }

        //So temperaure measure will stay the same during the user's session on the website
        [HttpPost]
        public ActionResult ParkDetail(bool isFarenheit)
        {
            Session["TemperatureMeasure"] = isFarenheit;

            return RedirectToAction("ParkDetail");
        }
    }
}