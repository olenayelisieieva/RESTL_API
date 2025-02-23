using System.Globalization;
using BankOperations.Infrastructure.Entities;
using CsvHelper;
using CsvHelper.Configuration;

namespace BankOperations.Context.Repositories
{
    public static class AccountsRepository
    {
        public static int GetLastId()
        {
            if (File.Exists("accounts.csv"))
            {
                using (var reader = new StreamReader("accounts.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<AccountEntity>();
                    var lastRecord = records.LastOrDefault();

                    if (lastRecord != null)
                    {
                        return lastRecord.Id;
                    }

                    return 0;
                }
            }

            return 0;
        }


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

        public static AccountEntity GetAccount(int id)
        {
            if (File.Exists("accounts.csv"))
            {
                using (var reader = new StreamReader("accounts.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<AccountEntity>();
                    var record = records.FirstOrDefault(account => account.Id == id);

                    if (record != null)
                    {
                        return record;
                    }

                    return new AccountEntity();
                }
            }

            return new AccountEntity();
        }

        public static void UpdateAccount(AccountEntity record)
        {
            if (File.Exists("accounts.csv"))
            {
                var records = new List<AccountEntity>();
                using (var reader = new StreamReader("accounts.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    records = csv.GetRecords<AccountEntity>().ToList();
                    var index = records.FindIndex(account => account.Id == record.Id);

                    if (index != -1)
                    {
                        records[index] = record;
                    }

                }

                using var writer = new StreamWriter("accounts.csv");
                using var csv2 = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv2.WriteRecords(records);
            }
        }

        public static List<AccountEntity> _accounts = new List<AccountEntity>();

        public static IEnumerable<AccountEntity> GetAllAccounts()
        {
            return _accounts;
        }

        public static bool DeleteAccount(int id)
        { 

            var records = new List<AccountEntity>();
            using (var reader = new StreamReader("accounts.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))

            {
                records = csv.GetRecords<AccountEntity>().ToList();
            }
            var recordToRemove = records.FirstOrDefault(a => a.Id == id);
            if (recordToRemove == null) return false;

            records.Remove(recordToRemove);

            using var writer = new StreamWriter("accounts.csv");
            using var csv2 = new CsvWriter(writer, CultureInfo.InvariantCulture);
            {
                csv2.WriteRecords(records);
            }

            return true;

        }

    }
}
