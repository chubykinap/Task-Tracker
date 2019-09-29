using System.Configuration;
using System.Web.Mvc;
using TaskTracker.Interfaces;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    public class HomeController : Controller
    {
        private IAccount service;

        public HomeController()
        {
            if (ConfigurationManager.AppSettings["save"].Equals("database"))
            {
                service = new DbAccountService();
            }
            else if (ConfigurationManager.AppSettings["save"].Equals("file"))
            {
                service = new FileAccountService("C:\\Users\\Александр Чубыкин\\source\\repos\\TaskTrackerNew 2\\Models\\Content\\FileStorage\\");
            }
        }

        [HttpGet]
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Index(string login, string password)
        {
            var account = service.Authorize(login, password);
            if (account != null)
            {
                Session.Add("accountId", account.Id);
                return Redirect("/Task/Tasks");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Account()
        {
            if (Session["accountId"] == null)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.Account = service.GetElement((int)Session["accountId"]);
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            if (Session["accountId"] != null)
            {
                ViewBag.Account = service.GetElement((int)Session["accountId"]);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Registration(string login,string password)
        {
            service.AddElement(new AccountModel
            {
                Login = login,
                Password = password
            });
            return Redirect("/Home/Index");
        }

        [HttpPost]
        public ActionResult Unreg()
        {
            Session["accountId"] = null;
            return Redirect("/Home/Index");
        }
    }
}