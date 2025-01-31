using System.Globalization;
using BankOperations.Infrastructure.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using BankOperations.Shared.Account;

namespace BankOperations.Context.Repositories
{
    public static class AccountsRepository
    {
        public static void AddAccount(List<AccountEntity> records)
        {
            if (File.Exists("accounts.csv"))
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };

                using (var stream = File.Open("accounts.csv", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(records);
                }
            }
            else
            {
                using (var writer = new StreamWriter("accounts.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
            }
        }
    }
}
