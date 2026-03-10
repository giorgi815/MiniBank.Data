using Minibank.Repository;
using Minibank.Repository.Models;

namespace MiniBank.Console.UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            var singleCustomer = customerRepository.GetCustomer(9);
            var allCustomers = customerRepository.GetCustomers();

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
