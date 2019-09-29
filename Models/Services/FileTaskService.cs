using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class FileTaskService : ITask
    {
        private readonly SerializationService service;

        public FileTaskService(string Path)
        {
            service = new SerializationService(Path);
        }

        public void AddElement(TaskModel task, int accountid)
        {
            List<TaskModel> tasks = service.Deserialize<TaskModel>();
            List<AccountModel> accounts = service.Deserialize<AccountModel>();
            List<ProjectModel> projects = service.Deserialize<ProjectModel>();
            AccountModel account = accounts.AsEnumerable()
                .FirstOrDefault(acc => acc.Id == accountid);
            ProjectModel project = projects.AsEnumerable()
                .FirstOrDefault(rec => rec.Id == -1);
            int maxId = tasks.Count == 0 ? 0 : tasks.Count;
            tasks.Add(new TaskModel
            {
                Id = maxId,
                Name = task.Name,
                Description = task.Description,
                Shedule = task.Shedule,
                Account = new Account
                {
                    Id = account.Id,
                    Login = account.Login,
                    Password = account.Password
                },
                Project = new Project
                {
                    Id = project.Id,
                    Name = project.Name
                },
                Status = TaskStatus.Незавершён
            });
            service.Serialize(tasks);
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

        public void AddTaskToProject(int projectId, TaskModel task)
        {
            List<ProjectModel> projects = service.Deserialize<ProjectModel>();
            ProjectModel project = projects.AsEnumerable()
                .FirstOrDefault(rec => rec.Id == projectId);
            List<TaskModel> tasks = service.Deserialize<TaskModel>();
            List<AccountModel> accounts = service.Deserialize<AccountModel>();
            AccountModel account = accounts.AsEnumerable()
                .FirstOrDefault(acc => acc.Id == task.Account.Id);
            if (project != null)
            {
                tasks.Add(new TaskModel
                {
                    Name = task.Name,
                    Description = task.Description,
                    Shedule = task.Shedule,
                    Account = new Account
                    {
                        Id = account.Id,
                        Login = account.Login,
                        Password = account.Password
                    },
                    Project = new Project
                    {
                        Id = project.Id,
                        Name = project.Name,
                        Description = project.Description,
                        Account = new Account
                        {
                            Id = account.Id,
                            Login = account.Login,
                            Password = account.Password
                        }
                    },
                    Status = TaskStatus.Незавершён
                });
                service.Serialize(tasks);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void ChangeData(TaskModel task)
        {
            List<TaskModel> tasks = service.Deserialize<TaskModel>();
            TaskModel element = tasks.AsEnumerable()
                .FirstOrDefault(rec => rec.Id == task.Id);
            if (element != null)
            {
                element.Name = task.Name;
                element.Description = task.Description;
                element.Shedule = task.Shedule;
                service.Serialize(tasks);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void DeleteElement(TaskModel task)
        {
            List<TaskModel> tasks = service.Deserialize<TaskModel>();
            TaskModel _task = tasks.AsEnumerable()
                 .FirstOrDefault(rec => rec.Id == task.Id);
            if (_task != null)
            {
                tasks.Remove(_task);
                service.Serialize(tasks);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        
        public TaskModel GetElement(int id)
        {
            List<TaskModel> tasks = service.Deserialize<TaskModel>();
            TaskModel task = tasks.AsEnumerable()
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
                    Account = task.Account,
                    Project = task.Project
                };
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<TaskModel> GetList(int accountId)
        {
            List<TaskModel> tasks = service.Deserialize<TaskModel>();
            List<TaskModel> result = tasks
                     .Where(rec => rec.Account.Id == accountId).AsEnumerable()
                     .Select(rec => new TaskModel
                     {
                         Id = rec.Id,
                         Name = rec.Name,
                         Description = rec.Description,
                         Shedule = rec.Shedule,
                         Status = rec.Status,
                         Account = rec.Account,
                         Project = rec.Project
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
    }
}