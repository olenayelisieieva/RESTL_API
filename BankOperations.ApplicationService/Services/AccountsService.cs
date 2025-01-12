using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankOperations.Context.Repositories;
using BankOperations.Infrastructure.Entities;

namespace BankOperations.ApplicationService.Services
{
    public static class AccountsService
    {
        public static void CreateAccount(AccountRequest accountRequest)
        {

            var records = new List<AccountEntity>() { };

            AccountsRepository.AddAccount(records);
        }
    }
}
