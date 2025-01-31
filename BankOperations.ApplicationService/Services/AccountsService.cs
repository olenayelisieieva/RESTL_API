using BankOperations.Context.Repositories;
using BankOperations.Infrastructure.Entities;
using BankOperations.Shared.Models.Requests;

namespace BankOperations.ApplicationService.Services
{
    public static class AccountsService
    {
        public static int CreateAccount(AccountRequest accountRequest)
        {

            var entity = new AccountEntity()
            {
                Id = GetNextId(),
                AccountId = Guid.NewGuid(),
                AccountName = accountRequest.AccountName,
                Balance = 0
            };

            var records = new List<AccountEntity>() { entity };

            AccountsRepository.AddAccount(records);

            return entity.Id;
        }

        public static void UpdateAccount(int id, AccountRequest accountRequest)
        {
            var record = AccountsRepository.GetAccount(id);
            record.AccountName = accountRequest.AccountName;
            AccountsRepository.UpdateAccount(record);

        }

        private static int GetNextId()
        {
            var currentId = AccountsRepository.GetLastId();
            return currentId + 1;
        }
    }
}
