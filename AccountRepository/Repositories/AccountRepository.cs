using AccountRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountRepository.Repositories
{
    internal class AccountRepository
    {
        private const string _filePath = @"../../../../Account.csv";
        private readonly List<Account> _accounts;
        public AccountRepository()
        {
            _accounts = LoadAccountData(_filePath);
        }

        public int AddAccount(Account newAccount)
        {
            if (newAccount == null)
                throw new ArgumentException(nameof(newAccount));

            var newId = _accounts.Any() ? _accounts.Max(x => x.Id) + 1 : 1;
            newAccount.Id = newId;

            _accounts.Add(newAccount);

            string newCsvLine = $"\n{newAccount.Id},{newAccount.AccountNumber},{newAccount.CustomerId},{newAccount.Balance},{newAccount.AccountType}";

            File.AppendAllText(_filePath, newCsvLine);

            return newId;


        }

        public Account GetAccount(int id) => _accounts.FirstOrDefault(x => x.Id == id);




        public List<Account> GetAccounts() => _accounts;

        public int DeleteAccount(int id)
        {
            var account = _accounts.FirstOrDefault(x => x.Id == id);

            if (account == null)
                return 0;

            _accounts.Remove(account);
            SaveData();

            return 1;

        }


        #region HELPERS

        private static List<Account> LoadAccountData(string filePath)
        {
            var accounts = new List<Account>();

            if (!File.Exists(filePath))
                return accounts;

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var account = FromCsv(line);
                if (account != null)
                    accounts.Add(account);


            }

            return accounts;
        }


        private static Account FromCsv(string line)
        {
            var part = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (part.Length != 5)
                throw new FormatException("Customer format is invalid");

            return new Account()
            {
                Id = int.Parse(part[0]),
                AccountNumber = part[1],
                CustomerId = int.Parse(part[2]),
                Balance = decimal.Parse(part[3]),
                AccountType = part[4]
            };

        }


        private void SaveData()
        {
            string header = "Id,AccountNumber,CustomerId,Balance,AccountType";

            var lines = new List<string> { header };

            lines.AddRange(_accounts.Select(x => $"{x.Id},{x.AccountNumber},{x.CustomerId},{x.Balance},{x.AccountType}"));

            File.WriteAllLines(_filePath, lines);

        }



        #endregion

    }
}
