using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface IProject
    {
        void AddElement(Project project);

        void ChangeData(Project project);

        void DeleteElement(Project project);

        Project GetElement(int id);

        List<Project> GetList();

        void AddTaskToProject(Task task, int projectId);

        List<Project> GetProjectsOfAccount(int accountId);
    }
}
