using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class DbAccountService : IAccount
    {
        private TrackerContext context;

        public DbAccountService(TrackerContext context)
        {
            this.context = context;
        }

        public DbAccountService()
        {
            context = new TrackerContext();
        }

        public void AddElement(AccountModel account)
        {
            Account _account = context.Accounts.FirstOrDefault(acc =>
            acc.Id == account.Id);
            if (_account == null)
            {
                context.Accounts.Add(new Account
                {
                    Login = account.Login,
                    Password = account.Password
                });
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Этот логин уже занят. Введите новый");
            }
        }

        public AccountModel Authorize(string login, string password)
        {
            Account account = context.Accounts
                .FirstOrDefault(rec => rec.Login == login);
            if (account != null)
            {
                if (account.Password != password)
                {
                    throw new Exception("Неверный пароль");
                }
                return new AccountModel
                {
                    Id = account.Id,
                    Login = account.Login,
                    Password = account.Password
                };
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void ChangeData(AccountModel account)
        {
            Account acc = context.Accounts
                .FirstOrDefault(rec => rec.Id == account.Id);
            if (acc != null)
            {
                acc.Password = account.Password;
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void DeleteElement(AccountModel account)
        {
            Account _account = context.Accounts
                .FirstOrDefault(rec => rec.Id == account.Id);
            if (_account != null)
            {
                context.Accounts.Remove(_account);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public AccountModel GetElement(int id)
        {
            Account acc = context.Accounts.FirstOrDefault(rec => rec.Id == id);
            if (acc != null)
            {
                return new AccountModel
                {
                    Id = acc.Id,
                    Login = acc.Login,
                    Password = acc.Password
                };
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<AccountModel> GetList()
        {
            return context.Accounts.Select(acc =>
            new AccountModel
            {
                Id = acc.Id,
                Login = acc.Login,
                Password = acc.Password
            }).ToList();
        }
    }
}