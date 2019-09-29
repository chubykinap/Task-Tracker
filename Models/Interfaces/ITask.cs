using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface ITask
    {
        void AddElement(TaskModel task, int accountid);

        void ChangeData(TaskModel task);

        void DeleteElement(TaskModel task);

        TaskModel GetElement(int id);

        List<TaskModel> GetList(int accountId);

        List<TaskModel> GetComingTasks(int accountId,int start);

        void AddTaskToProject(int projectId, TaskModel task);

        void AddNewTask(string name, string description, string date, int? projectId, int accountId);
    }
}
