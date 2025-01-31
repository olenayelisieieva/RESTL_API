using BankOperations.Context.Repositories;
using BankOperations.Infrastructure.Entities;
using BankOperations.Shared.Models.Requests;

namespace BankOperations.ApplicationService.Services
{
    public static class AccountsService
    {
        public static void CreateAccount(AccountRequest accountRequest)
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
        }

        private static int GetNextId()
        {
            var currentId = AccountsRepository.GetLastId();
            return currentId + 1;
        }
    }
}
