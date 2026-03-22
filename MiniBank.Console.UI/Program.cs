using AccountRepository.Models;
using Minibank.Repository;
using Minibank.Repository.Models;
using AccountRepository.Repositories;

namespace MiniBank.Console.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            var singleCustomer = customerRepository.GetCustomer(9);
            var allCustomers = customerRepository.GetCustomers();
            OperationRepository operationRepository = new OperationRepository();


            var transfer = operationRepository.Transfer(30, 29, 100);


            //var repo = new AccountRepository.Repositories.AccountRepository();

            //var deposit = operationRepository.Deposit(30, 2000);
            //var withdraw = operationRepository.Withdraw(30, 400);

            //var account = new Account
            //{
            //    Balance = 1000,
            //    CustomerId = 3,
            //    Currency = "gel",
            //    Id = 30,
            //    Iban = "GE56589521475235541948",
            //    Name = "personal"

            //};

            //repo.AddAccount(account);

            //repo.DeleteAccount(31);

            //var addCustomer = customerRepository.AddCustomer(new Minibank.Repository.Models.Customer()
            //{
            //    Id = 32,
            //    Name = "random1",
            //    Email = "randsadas1@asdma.com",
            //    IdentityNumber = "12345678942125",
            //    PhoneNumber = "21732131",
            //    CustomerType = 0 //PHYISICAL PERSON
            //});


            //var updateCustomer = customerRepository.UpdateCustomer(new Minibank.Repository.Models.Customer()
            //{
            //    Id = 13,
            //    Name = "random12",
            //    Email = "randsadas18@asdma.com",
            //    IdentityNumber = "12345678942125",
            //    PhoneNumber = "21732131",
            //    CustomerType = 0 //PHYISICAL PERSON
            //});


            //var deleteCustomer = customerRepository.DeleteCustomer(14);

        }
    }
}
