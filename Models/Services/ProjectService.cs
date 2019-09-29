using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class ProjectService : IProject
    {
        private readonly SerializationService service;

        public ProjectService(string Path)
        {
            service = new SerializationService(Path);
        }

        public void AddElement(ProjectModel project, int accountid)
        {
            List<ProjectModel> projects = service.Deserialize<ProjectModel>();
            List<AccountModel> accounts = service.Deserialize<AccountModel>();
            AccountModel account = accounts
                .FirstOrDefault(acc => acc.Id == accountid);
            ProjectModel proj = projects
                .FirstOrDefault(rec => rec.Name == project.Name);
            int maxId = projects.Count == 0 ? 0 : projects.Count;
            if (proj == null)
            {
                projects.Add(new ProjectModel
                {
                    Id = maxId,
                    Name = project.Name,
                    Description = project.Description,
                    Account = new Account
                    {
                        Id = account.Id,
                        Login = account.Login,
                        Password = account.Password
                    }
                });
                service.Serialize(projects);
            }
            else
            {
                throw new Exception("Проект с таким именем уже существует");
            }
        }


        public void ChangeData(ProjectModel project)
        {
            List<ProjectModel> projects = service.Deserialize<ProjectModel>();
            ProjectModel proj = projects
                .FirstOrDefault(rec => rec.Id == project.Id);
            if (proj != null)
            {
                proj.Name = project.Name;
                proj.Description = project.Description;
                service.Serialize(projects);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void DeleteElement(ProjectModel project)
        {
            List<ProjectModel> projects = service.Deserialize<ProjectModel>();
            ProjectModel proj = projects
                .FirstOrDefault(rec => rec.Id == project.Id);
            AccountModel account = service.Deserialize<AccountModel>()
                .FirstOrDefault(acc => acc.Id == project.Account.Id);
            if (proj != null)
            {
                List<TaskModel> tasks = service.Deserialize<TaskModel>();
                List<TaskModel> tasksInProject = tasks
                    .Where(task => task.Project.Id == project.Id)
                    .Select(task => new TaskModel
                    {
                        Id = task.Id,
                        Name = task.Name,
                        Description = task.Description,
                        Shedule = task.Shedule,
                        Status = task.Status,
                        Account = new Account
                        {
                            Id = account.Id,
                            Login = account.Login,
                            Password = account.Password
                        },
                        Project = new Project
                        {
                            Id = proj.Id,
                            Name = proj.Name,
                            Description = proj.Description,
                            Account = new Account
                            {
                                Id = account.Id,
                                Login = account.Login,
                                Password = account.Password
                            }
                        }
                    }).ToList();
                tasks.RemoveAll(task => tasksInProject.Exists(t => t.Id == task.Id));
                projects.Remove(proj);
                service.Serialize(projects);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ProjectModel GetElement(int id)
        {
            if (id >= 0)
            {
                List<ProjectModel> projects = service.Deserialize<ProjectModel>();
                ProjectModel project = projects
                    .FirstOrDefault(rec => rec.Id == id);
                if (project != null)
                {
                    return new ProjectModel
                    {
                        Id = project.Id,
                        Name = project.Name,
                        Description = project.Description,
                        Account = project.Account
                    };
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
            return new ProjectModel { Id = -1, Name = "Нет" };
        }

        public List<ProjectModel> GetList(int accountId)
        {
            List<ProjectModel> projects = service.Deserialize<ProjectModel>();
            List<ProjectModel> result = new List<ProjectModel>();
            if (projects.Count != 0)
            {
                result = projects
                           .Where(rec => rec.Account.Id == accountId)
                           .Select(rec => new ProjectModel
                           {
                               Id = rec.Id,
                               Name = rec.Name,
                               Description = rec.Description,
                               Account = rec.Account
                           }).ToList();
            }
            return result;
        }

        public List<TaskModel> GetTaskInProject(int projectId)
        {
            List<TaskModel> tasks = service.Deserialize<TaskModel>();
            List<TaskModel> result = tasks
                   .Where(rec => rec.Project.Id == projectId)
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
    }
}