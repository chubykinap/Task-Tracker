using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using TaskTracker.Interfaces;
using TaskTracker.Models;
using TaskTracker.Services;

namespace TaskTracker.Controllers
{
    public class TaskController : Controller
    {
        private ITask service;

        public TaskController()
        {
            if (ConfigurationManager.AppSettings["save"].Equals("database"))
            {
                service = new DbTaskService();
            }
            else if (ConfigurationManager.AppSettings["save"].Equals("file"))
            {
                service = new FileTaskService("C:\\Users\\Александр Чубыкин\\source\\repos\\TaskTrackerNew 2\\Models\\Content\\FileStorage\\");
            }
        }

        [HttpGet]
        public ActionResult Tasks()
        {
            if (Session["accountId"] == null)
            {
                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Task(int id)
        {
            if (Session["accountId"] == null)
            {
                return Redirect("/Home/Index");
            }
            Session.Add("taskId", id);
            ViewBag.Task = service.GetElement(id);
            return View();
        }

        [HttpGet]
        public ActionResult AddTask()
        {
            if (Session["accountId"] == null)
            {
                return Redirect("/Home/Index");
            }
            if (Session["taskId"] != null)
            {
                ViewBag.Task = service.GetElement((int)Session["taskId"]);
            }
            Session.Remove("taskId");
            return View();
        }

        [HttpPost]
        public ActionResult AddTask(string name, string description, string date, int? projectId)
        {
            service.AddNewTask(name, description, date, projectId, (int)Session["accountId"]);
            return Redirect("/Task/Tasks");
        }

        [HttpPost]
        public ActionResult CompleteTask()
        {
            service.DeleteElement(new TaskModel
            {
                Id = (int)Session["taskId"]
            });
            Session.Remove("taskId");
            return Redirect("/Task/Tasks");
        }

        public ActionResult DisplayTask(int start)
        {
            return PartialView(service.GetComingTasks((int)Session["accountId"], start));
        }
    }
}