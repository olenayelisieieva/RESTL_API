using System.Net.Http;
using BankOperations.API.Controllers.services;
using BankOperations.Context.Repositories;
using BankOperations.Infrastructure.Entities;
using BankOperations.Shared.Models.Contracts;
using BankOperations.Shared.Models.Requests;
using Microsoft.Extensions.Logging;

namespace BankOperations.ApplicationService.Services
{
    public class AccountsService
    {
        private readonly AccountsRepository _accountsRepository;
        private readonly ScopedService _scoped;
        private readonly TransientService _transietn;
        private readonly SingletonService _singleton;

        public AccountsService(AccountsRepository accountsRepository,
            ScopedService scoped,
            TransientService transietn,
            SingletonService singleton)
        {
            _accountsRepository = accountsRepository;
            _scoped = scoped;
            _transietn = transietn;
            _singleton = singleton;
        }

        public int CreateAccount(AccountRequest accountRequest)
        {

            Console.WriteLine("FROM ACCOUNT SERVICE");
            Console.WriteLine("SCOPED: " + _scoped.CurrentGuid.ToString());
            Console.WriteLine("TRANSIENT: " + _transietn.CurrentGuid.ToString());
            Console.WriteLine("SINGLETON: " + _singleton.CurrentGuid.ToString());

            var entity = new AccountEntity()
            {
                Id = GetNextId(),
                AccountId = Guid.NewGuid(),
                AccountName = accountRequest.AccountName,
                Balance = 0
            };

            var records = new List<AccountEntity>() { entity };

            _accountsRepository.AddAccount(records);

            return entity.Id;
        }

        public void UpdateAccount(int id, AccountRequest accountRequest)
        {
            var record = _accountsRepository.GetAccount(id);
            record.AccountName = accountRequest.AccountName;
            _accountsRepository.UpdateAccount(record);

        }

        private int GetNextId()
        {
            var currentId = _accountsRepository.GetLastId();
            return currentId + 1;
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            var entities = _accountsRepository.GetAllAccounts();
            var results = new List<Account>();

            foreach (var item in entities)
            {
                var account = new Account()
                {
                    Name = item.AccountName,
                    Id = item.Id
                };

                results.Add(account);
            }

            return results;
        }

        public Account GetAccountById(int id)
        {
            var accountEntity = _accountsRepository.GetAccount(id);

            if (accountEntity == null) 
            { 
                return new Account();
            }

            var account = new Account()
            {
                Id = accountEntity.Id,
                Name = accountEntity.AccountName,
            };

            return account;
        }

        public bool DeleteAccount(int id)
        {
            return _accountsRepository.DeleteAccount(id);
        }


    }
}
