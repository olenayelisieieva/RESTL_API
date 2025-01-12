using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankOperations.Infrastructure.Entities
{
    public class AccountEntity
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public Guid AccountId { get; set; }
        public int Balance { get; set; } = 0;
    }
}
