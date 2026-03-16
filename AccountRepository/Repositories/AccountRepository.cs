using AccountRepository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AccountRepository.Repositories
{
    public class AccountRepository
    {
        private const string _filePath = "C:\\Users\\gioch\\source\\repos\\MiniBank.Data\\Account.Data\\Accounts.json";
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


            SaveData();

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


        public int UpdateAccount(Account UpdateAccount)
        {
            var accounts = _accounts.FirstOrDefault(x => x.Id == UpdateAccount.Id);

            if (accounts == null)
                throw new Exception("Account not found");

            SaveData();


            return 1;
        }



        #region HELPERS

        private static List<Account> LoadAccountData(string filePath)
        {

            if (!File.Exists(filePath))
                return new List<Account>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Account>>(json) ?? new List<Account>();

        }





        private void SaveData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_accounts, options);
            File.WriteAllText(_filePath, json);
        }



        #endregion

    }
}
