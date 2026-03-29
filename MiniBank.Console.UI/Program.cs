using AccountRepository.Models;
using Minibank.Repository;
using Minibank.Repository.Models;
using AccountRepository.Repositories;

namespace MiniBank.Console.UI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {



            var repo = await AccountRepository.Repositories.AccountRepository.CreateAsync();


            //var account = new Account
            //{
            //    Name = "name1",
            //    Id = 9,
            //    CustomerId = 2,
            //    Balance = 100000,
            //    Currency = "EUR",
            //    Iban = "GE55SB7785625594124549"
            //};

            //repo.AddAccount(account);





        }
    }
}
