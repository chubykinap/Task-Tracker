using System.Configuration;
using System.Web.Mvc;
using TaskTracker.Interfaces;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    public class ProjectController : Controller
    {
        private IProject service;

        public ProjectController()
        {
            if (ConfigurationManager.AppSettings["save"].Equals("database"))
            {
                service = new DbProjectService();
            }
            else if (ConfigurationManager.AppSettings["save"].Equals("file"))
            {
                service = new FileProjectService("C:\\Users\\Александр Чубыкин\\source\\repos\\TaskTrackerNew 2\\Models\\Content\\FileStorage\\");
            }
        }

        [HttpGet]
        public ActionResult Projects()
        {
            if (Session["accountId"] == null)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.Projects = service.GetList((int)Session["accountId"]);
            return View();
        }

        [HttpGet]
        public ActionResult Project(int? id)
        {
            if (Session["accountId"] == null)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.Project = service.GetElement(id.Value);
            ViewBag.Tasks = service.GetTaskInProject(id.Value);
            return View();
        }

        [HttpGet]
        public ActionResult AddProject()
        {
            if (Session["accountId"] == null)
            {
                return Redirect("/Home/Index");
            }
            if (Session["projectId"] != null)
            {
                ViewBag.Project = service.GetElement((int)Session["taskId"]);
            }
            Session.Remove("projectId");
            return View();
        }

        [HttpPost]
        public ActionResult AddProject(string name, string description)
        {
            service.AddElement(new ProjectModel
            {
                Name = name,
                Description = description
            }, (int)Session["accountId"]);
            return Redirect("/Project/Projects");
        }
    }
}