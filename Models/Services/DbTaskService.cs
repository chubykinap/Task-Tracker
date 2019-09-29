using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class DbTaskService : ITask
    {
        private TrackerContext context;

        public DbTaskService(TrackerContext context)
        {
            this.context = context;
        }

        public DbTaskService()
        {
            context = new TrackerContext();
        }

        public void AddElement(TaskModel task, int accountid)
        {
            context.Tasks.Add(new Task
            {
                Name = task.Name,
                Description = task.Description,
                Shedule = task.Shedule,
                Account = context.Accounts
                .FirstOrDefault(acc => acc.Id == accountid),
                Project = context.Projects
                .FirstOrDefault(rec => rec.Id == -1),
                Status = TaskStatus.Незавершён
            });
            context.SaveChanges();
        }

        public void AddTaskToProject(int projectId, TaskModel task)
        {
            Project project = context.Projects.AsEnumerable()
                .FirstOrDefault(rec => rec.Id == projectId);
            if (project != null)
            {
                context.Tasks.Add(new Task
                {
                    Name = task.Name,
                    Description = task.Description,
                    Shedule = task.Shedule,
                    Account = context.Accounts
                            .FirstOrDefault(acc => acc.Id == project.Account.Id),
                    Project = project,
                    Status = TaskStatus.Незавершён
                });
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void ChangeData(TaskModel task)
        {
            Task element = context.Tasks.AsEnumerable()
                .FirstOrDefault(rec => rec.Id == task.Id);
            if (element != null)
            {
                element.Name = task.Name;
                element.Description = task.Description;
                element.Shedule = task.Shedule;
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void DeleteElement(TaskModel task)
        {
            Task _task = context.Tasks.AsEnumerable()
                 .FirstOrDefault(rec => rec.Id == task.Id);
            if (_task != null)
            {
                context.Tasks.Remove(_task);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public TaskModel GetElement(int id)
        {
            Task task = context.Tasks
                .FirstOrDefault(rec => rec.Id == id);
            if (task != null)
            {
                return new TaskModel
                {
                    Id = task.Id,
                    Name = task.Name,
                    Description = task.Description,
                    Shedule = task.Shedule,
                    Status = task.Status,
                    Account = context.Accounts.AsEnumerable()
                            .FirstOrDefault(acc => acc.Id == task.Account.Id),
                    Project = context.Projects.AsEnumerable()
                            .FirstOrDefault(acc => acc.Id == task.Project.Id)
                };
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<TaskModel> GetList(int accountId)
        {
            List<TaskModel> result = context.Tasks.AsEnumerable()
                     .Where(rec => rec.Account.Id == accountId)
                     .Select(rec => new TaskModel
                     {
                         Id = rec.Id,
                         Name = rec.Name,
                         Description = rec.Description,
                         Shedule = rec.Shedule,
                         Status = rec.Status,
                         Account = context.Accounts
                            .FirstOrDefault(acc => acc.Id == rec.Account.Id),
                         Project = context.Projects
                            .FirstOrDefault(acc => acc.Id == rec.Project.Id)
                     }).ToList();
            return result;
        }

        public List<TaskModel> GetComingTasks(int accountId, int start)
        {
            DateTime coming = DateTime.Now.AddDays(7);
            List<TaskModel> result = new List<TaskModel>();
            List<TaskModel> res = GetList(accountId).AsEnumerable()
                .Where(rec => rec.Shedule < coming)
                .OrderBy(rec => rec.Shedule).ToList();
            if (start > res.Count)
                start = res.Count;
            for (int count = 0; count < start; count++)
            {
                result.Add(res[count]);
            }
            return result;
        }

        public void AddNewTask(string name, string description,
            string date, int? projectId, int accountId)
        {
            if (projectId.HasValue)
            {
                AddTaskToProject(projectId.Value, new TaskModel
                {
                    Name = name,
                    Description = description,
                    Shedule = DateTime.Parse(date),
                    Project = new Project
                    {
                        Id = projectId.Value
                    },
                    Account = new Account
                    {
                        Id = accountId
                    }
                });
            }
            else
            {
                AddElement(new TaskModel
                {
                    Name = name,
                    Description = description,
                    Shedule = DateTime.Parse(date),
                }, accountId);
            }
        }
    }
}