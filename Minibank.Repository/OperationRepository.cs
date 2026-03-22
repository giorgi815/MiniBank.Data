using AccountRepository.Models;
using AccountRepository.Models.Enums;
using AccountRepository.Repositories;


namespace Minibank.Repository
{
    public class OperationRepository
    {
        private AccountRepository.Repositories.AccountRepository _accountRepository;
        private readonly List<Operation> _operation;
        private string filePath = @"../../../../Operation.xml";



        //transfer, deposit, withdraw

        public OperationRepository()
        {
            _accountRepository = new AccountRepository.Repositories.AccountRepository();
        }

        public int Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = _accountRepository.GetAccount(fromAccountId);
            var toAccount = _accountRepository.GetAccount(toAccountId);


            if (fromAccount == null || toAccount == null)
                throw new Exception("Account not found");

            if (fromAccount.Balance < amount)
                throw new Exception("Inssuficient balance");



            //Operation from = new Operation()
            //{
            //    Id = _operation.Any() ? _operation.Max(x => x.Id) + 1 : 1,
            //    AcountId = fromAccountId,
            //    Amount = -amount,
            //    HappendAt = DateTime.Now,
            //    operationType = OperationType.debit
            //};

            //Operation to = new Operation()
            //{
            //    Id = _operation.Any() ? _operation.Max(x => x.Id) + 1 : 1,
            //    AcountId = fromAccountId,
            //    Amount = amount,
            //    HappendAt = DateTime.Now,
            //    operationType = OperationType.debit
            //};

            var updateSender = fromAccount.Balance -= amount;
            var updateReciver = toAccount.Balance += amount;

            _accountRepository.UpdateAccount(fromAccount);
            _accountRepository.UpdateAccount(toAccount);

            return 1;

        }

        public int Withdraw(int accountId, decimal amount)
        {
            var fromAccount = _accountRepository.GetAccount(accountId);

            if (fromAccount == null)
                throw new Exception("Account not founf");

            if (fromAccount.Balance < amount)
                throw new Exception("Inssuficient balance");

            fromAccount.Balance -= amount;

            _accountRepository.UpdateAccount(fromAccount);

            return 1;

        }

        public int Deposit(int accountId, decimal amount)
        {
            var fromAccount = _accountRepository.GetAccount(accountId);

            if (fromAccount == null)
                throw new Exception("Account not found");

            fromAccount.Balance += amount;

            _accountRepository.UpdateAccount(fromAccount);

            return 1;
        }

    }
}
