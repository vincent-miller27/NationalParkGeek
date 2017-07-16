using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";

        // GET: Survey
        public ActionResult Index()
        {
            SurveySqlDAL surveyDAL = new SurveySqlDAL(connectionString);
            List<Survey> surveyList = surveyDAL.GetSurveys();

            return View("Index", surveyList);
        }

        public ActionResult SurveySubmit()
        {
            return View("SurveySubmit");
        }

        [HttpPost]
        public ActionResult SurveySubmit(SurveyForm surveyForm)
        {
            if (!ModelState.IsValid)
            {
                return View("SurveySubmit", surveyForm);
            }

            SurveySqlDAL surveyDAL = new SurveySqlDAL(connectionString);
            surveyDAL.SubmitSurvey(surveyForm);

            TempData["StatusMessage"] = "Your Survey Has Been Saved";

            return RedirectToAction("Index");
        }
    }
}