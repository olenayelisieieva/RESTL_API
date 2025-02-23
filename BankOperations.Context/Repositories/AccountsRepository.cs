using System.Globalization;
using BankOperations.Infrastructure.Entities;
using CsvHelper;
using CsvHelper.Configuration;

namespace BankOperations.Context.Repositories
{
    public class AccountsRepository
    {
        public int GetLastId()
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


        public void AddAccount(List<AccountEntity> records)
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

        public AccountEntity GetAccount(int id)
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

        public void UpdateAccount(AccountEntity record)
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

        public List<AccountEntity> _accounts = new List<AccountEntity>();

        public IEnumerable<AccountEntity> GetAllAccounts()
        {
            var result = new List<AccountEntity>();
            if (File.Exists("accounts.csv"))
            {
                using (var reader = new StreamReader("accounts.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<AccountEntity>().ToList();

                    if (records != null)
                    {
                        result = records;
                    }

                }
            }

            return result;
        }

        public bool DeleteAccount(int id)
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
