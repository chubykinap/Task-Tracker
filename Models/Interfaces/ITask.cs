using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface ITask
    {
        void AddElement(Task task);

        void ChangeData(Task task);

        void DeleteElement(Task task);

        Task GetElement(int id);

        List<Task> GetList();

        List<Task> GetComing();

        List<Task> GetTaskInProject(int projectId);
    }
}
