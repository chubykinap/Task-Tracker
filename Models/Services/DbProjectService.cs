using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class DbProjectService : IProject
    {
        private TrackerContext context;

        public DbProjectService(TrackerContext context)
        {
            this.context = context;
        }

        public DbProjectService()
        {
            context = new TrackerContext();
        }

        public void AddElement(ProjectModel project, int accountid)
        {
            Project proj = context.Projects.FirstOrDefault(rec => rec.Name == project.Name);
            if (proj == null)
            {
                context.Projects.Add(new Project
                {
                    Name = project.Name,
                    Description = project.Description,
                    Account = context.Accounts
                         .FirstOrDefault(acc => acc.Id == accountid)
                });
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Проект с таким именем уже существует");
            }
        }

        public void ChangeData(ProjectModel project)
        {
            Project proj = context.Projects
                .FirstOrDefault(rec => rec.Id == project.Id);
            if (proj != null)
            {
                proj.Name = project.Name;
                proj.Description = project.Description;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void DeleteElement(ProjectModel project)
        {
            Project proj = context.Projects
                .FirstOrDefault(rec => rec.Id == project.Id);
            if (proj != null)
            {
                context.Projects.Remove(proj);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public ProjectModel GetElement(int id)
        {
            Project project = context.Projects
                .FirstOrDefault(rec => rec.Id == id);
            if (project != null)
            {
                return new ProjectModel
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    Account = context.Accounts
                            .FirstOrDefault(acc => acc.Id == project.Account.Id)
                };
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<ProjectModel> GetList(int accountId)
        {
            List<ProjectModel> result = context.Projects
                       .Where(rec => rec.Account.Id == accountId)
                       .Select(rec => new ProjectModel
                       {
                           Id = rec.Id,
                           Name = rec.Name,
                           Description = rec.Description,
                           Account = context.Accounts
                            .FirstOrDefault(acc => acc.Id == rec.Account.Id)
                       }).ToList();
            return result;
        }

        public List<TaskModel> GetTaskInProject(int projectId)
        {
            List<TaskModel> result = context.Tasks
                   .Where(rec => rec.Project.Id == projectId)
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
    }
}