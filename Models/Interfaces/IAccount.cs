using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface IAccount
    {
        void AddElement(AccountModel account);

        void ChangeData(AccountModel account);

        void DeleteElement(AccountModel account);

        AccountModel GetElement(int id);

        List<AccountModel> GetList();

        AccountModel Authorize(string login, string password);
    }
}
