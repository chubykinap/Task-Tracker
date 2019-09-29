using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface IProject
    {
        void AddElement(ProjectModel project, int accountid);

        void ChangeData(ProjectModel project);

        void DeleteElement(ProjectModel project);

        ProjectModel GetElement(int id);

        List<ProjectModel> GetList(int accountId);
        
        List<TaskModel> GetTaskInProject(int projectId);
    }
}
