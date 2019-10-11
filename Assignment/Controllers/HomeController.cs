using Assignment.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Assignment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        private GithubSearchObject FetchDataFromGitHubApi(string dateRange)
        {
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://api.github.com/search/repositories?q=created:" + dateRange + "&sort=stars&order=desc&per_page=25");
            Request.UserAgent = "pratikshya07";
            Request.Accept = "application/json";

            return ConvertJsonDataToObject((HttpWebResponse)Request.GetResponse());
        }

        private GithubSearchObject ConvertJsonDataToObject(HttpWebResponse Response)
        {
            using (var reader = new StreamReader(Response.GetResponseStream()))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                return (GithubSearchObject)js.Deserialize(reader.ReadToEnd(), typeof(GithubSearchObject));
            }
        }

        [HttpGet]
        public ActionResult TopRepositories(char type = ' ') {
            var dateRange = GetDateToSearchFromType(type);
            ViewBag.SelectedRange = dateRange[1];

            return View(FetchDataFromGitHubApi(dateRange[0]));
        }

        private string[] GetDateToSearchFromType(char type)
        {
            var today = DateTime.Today;
            var returnStr = ".." + today.ToString("yyyy-MM-dd'T'HH:mm:ss");

            switch (type)
            {
                case 'Y':
                    return new string[] { today.AddYears(-1).ToString("yyyy-MM-dd'T'HH:mm:ss") + returnStr, "Yearly"};
                case 'M':
                    return new string[] { today.AddMonths(-1).ToString("yyyy-MM-dd'T'HH:mm:ss") + returnStr, "Monthly" };
                case 'W':
                    return new string[] { today.AddDays(-7).ToString("yyyy-MM-dd'T'HH:mm:ss") + returnStr, "Weekly" };
                default:
                    return new string[] { today.AddDays(-1).ToString("yyyy-MM-dd'T'HH:mm:ss") + returnStr, "Daily" };
            }
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
    }
}