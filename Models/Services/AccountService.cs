using System;
using System.Collections.Generic;
using System.Linq;
using TaskTracker.Interfaces;
using TaskTracker.Models;

namespace TaskTracker.Services
{
    public class AccountService : IAccount
    {
        private readonly SerializationService service;

        public AccountService(string Path)
        {
            service = new SerializationService(Path);
        }

        public void AddElement(AccountModel account)
        {
            List<AccountModel> accounts = service.Deserialize<AccountModel>();
            AccountModel _account = accounts.FirstOrDefault(acc =>
            acc.Id == account.Id);
            int maxId = accounts.Count == 0 ? 0 : accounts.Count;
            if (_account == null)
            {
                accounts.Add(new AccountModel
                {
                    Id = maxId,
                    Login = account.Login,
                    Password = account.Password
                });
                service.Serialize(accounts);
            }
            else
            {
                throw new Exception("Этот логин уже занят. Введите новый");
            }
        }

        public AccountModel Authorize(string login, string password)
        {
            List<AccountModel> accounts = service.Deserialize<AccountModel>();
            AccountModel account = accounts
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
            List<AccountModel> accounts = service.Deserialize<AccountModel>();
            AccountModel acc = accounts
                .FirstOrDefault(rec => rec.Id == account.Id);
            if (acc != null)
            {
                acc.Password = account.Password;
                service.Serialize(accounts);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public void DeleteElement(AccountModel account)
        {
            List<AccountModel> accounts = service.Deserialize<AccountModel>();
            AccountModel _account = accounts
                .FirstOrDefault(rec => rec.Id == account.Id);
            if (_account != null)
            {
                accounts.Remove(_account);
                service.Serialize(accounts);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public AccountModel GetElement(int id)
        {
            List<AccountModel> accounts = service.Deserialize<AccountModel>();
            AccountModel acc = accounts.FirstOrDefault(rec => rec.Id == id);
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
            List<AccountModel> accounts = service.Deserialize<AccountModel>();
            return accounts.Select(acc =>
            new AccountModel
            {
                Id = acc.Id,
                Login = acc.Login,
                Password = acc.Password
            }).ToList();
        }
    }
}