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
        public AccountRepository(List<Account> accounts)
        {
            _accounts = accounts;
        }

        public static async Task<AccountRepository> CreateAsync()
        {
            var accounts = await LoadAccountData(_filePath);
            return new AccountRepository(accounts);
        }


        public async Task<int> AddAccount(Account newAccount)
        {
            if (newAccount == null)
                throw new ArgumentException(nameof(newAccount));

            var newId = _accounts.Any() ? _accounts.Max(x => x.Id) + 1 : 1;
            newAccount.Id = newId;

            _accounts.Add(newAccount);


            await SaveData();

            return newId;


        }

        public Account GetAccount(int id) => _accounts.FirstOrDefault(x => x.Id == id);




        public List<Account> GetAccounts() => _accounts;

        public async Task<int> DeleteAccount(int id)
        {
            var account = _accounts.FirstOrDefault(x => x.Id == id);

            if (account == null)
                return 0;

            _accounts.Remove(account);
            await SaveData();

            return 1;

        }


        public async Task<int> UpdateAccount(Account UpdateAccount)
        {
            var accounts = _accounts.FirstOrDefault(x => x.Id == UpdateAccount.Id);

            if (accounts == null)
                throw new Exception("Account not found");

            await SaveData();


            return 1;
        }



        #region HELPERS

        private static async Task<List<Account>> LoadAccountData(string filePath)
        {

            if (!File.Exists(filePath))
                return new List<Account>();

            using FileStream stream = File.OpenRead(filePath);
            var data = await JsonSerializer.DeserializeAsync<List<Account>>(stream) ?? new List<Account>();

            return data ?? new List<Account>();

        }





        private async Task SaveData()
        {
            using FileStream stream = File.Create(_filePath);

            await JsonSerializer.SerializeAsync(stream, _accounts, new JsonSerializerOptions
            {
                WriteIndented = true
            });

        }



        #endregion

    }
}
