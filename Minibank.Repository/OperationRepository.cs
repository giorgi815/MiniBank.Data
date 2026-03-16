using AccountRepository.Models;
using AccountRepository.Repositories;


namespace Minibank.Repository
{
    public class OperationRepository
    {
        private AccountRepository.Repositories.AccountRepository _accountRepository;


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

            fromAccount.Balance -= amount;
            toAccount.Balance += amount;

            _accountRepository.UpdateAccount(fromAccount);
            _accountRepository.UpdateAccount(toAccount);

            return 1;

        }

        public int Withdraw(int fromAccountId, decimal amount)
        {
            var fromAccount = _accountRepository.GetAccount(fromAccountId);

            if (fromAccount == null)
                throw new Exception("Account not founf");

            if (fromAccount.Balance < amount)
                throw new Exception("Inssuficient balance");

            fromAccount.Balance -= amount;

            return 1;

        }

        public int Deposit(int fromAccountId, decimal amount)
        {
            var fromAccount = _accountRepository.GetAccount(fromAccountId);

            if (fromAccount == null)
                throw new Exception("Account not found");

            fromAccount.Balance += amount;

            return 1;
        }

    }
}
