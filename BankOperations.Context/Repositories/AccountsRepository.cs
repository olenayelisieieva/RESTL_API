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
    }
}
