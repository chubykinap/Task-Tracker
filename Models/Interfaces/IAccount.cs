using System.Collections.Generic;
using TaskTracker.Models;

namespace TaskTracker.Interfaces
{
    public interface IAccount
    {
        void AddElement(Account account);

        void ChangeData(Account account);

        void DeleteElement(Account account);

        Account GetElement(int id);

        List<Account> GetList();

        Account Authorize(string login, string password);
    }
}
