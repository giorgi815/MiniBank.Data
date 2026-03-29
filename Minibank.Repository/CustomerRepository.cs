using Minibank.Repository.Models;

namespace Minibank.Repository
{
    public class CustomerRepository
    {
        private const string _filePath = @"../../../../MiniBank.Data/Customers.csv";
        private readonly List<Customer> _customers;

        public CustomerRepository(List<Customer> customers)
        {
            _customers = customers;
        }

        public static async Task<CustomerRepository> CreateAsync(string filePath)
        {

            var customers = await LoadData(filePath);
            return new CustomerRepository(customers);

        }


        public async Task<int> AddCustomer(Customer newCustomer)
        {
            if (newCustomer == null)
                throw new ArgumentException(nameof(newCustomer));

            var newId = _customers.Any() ? _customers.Max(x => x.Id) + 1 : 1;
            newCustomer.Id = newId;


            _customers.Add(newCustomer);

            await SaveData();

            return newId;
        }

        public Customer GetCustomer(int id) => _customers.FirstOrDefault(c => c.Id == id);

        public List<Customer> GetCustomers() => _customers;


        public async Task<int> UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentException(nameof(customer));

            var existingCustomer = _customers.FirstOrDefault(c => c.Id == customer.Id);

            if (existingCustomer == null)
                return 0;

            existingCustomer.Name = customer.Name;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.IdentityNumber = customer.IdentityNumber;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Email = customer.Email;
            existingCustomer.CustomerType = customer.CustomerType;

            await SaveData();

            return 1;
        }

        public async Task<int> DeleteCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
                return 0;

            _customers.Remove(customer);
            await SaveData();

            return 1;
        }


        #region HELPERS

        private static async Task<List<Customer>> LoadData(string filePath)
        {
            var customers = new List<Customer>();

            if (!File.Exists(filePath))
                return customers;

            using var reader = new StreamReader(filePath);

            await reader.ReadLineAsync();

            string? line;


            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var customer = FromCsv(line);
                if (customer != null)
                    customers.Add(customer);
            }

            return customers;
        }
        private static Customer FromCsv(string line)
        {
            var parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 6)
                throw new FormatException("Customer format is invalid");

            return new Customer()
            {
                Id = int.Parse(parts[0]),
                Name = parts[1],
                IdentityNumber = parts[2],
                PhoneNumber = parts[3],
                Email = parts[4],
                CustomerType = int.Parse(parts[5])
            };

        }

        private async Task SaveData()
        {
            string header = "Id,Name,IdentityNumber,PhoneNumber,Email,CustomerType";

            var lines = new List<string> { header };

            lines.AddRange(_customers.Select(c => $"{c.Id},{c.Name},{c.IdentityNumber},{c.PhoneNumber},{c.Email},{c.CustomerType}"));

            await File.WriteAllLinesAsync(_filePath, lines);

        }



        #endregion
    }
}
