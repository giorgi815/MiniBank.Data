using AccountRepository.Models;
using AccountRepository.Models.Enums;
using AccountRepository.Repositories;
using System.Xml.Linq;


namespace Minibank.Repository
{
    public class OperationRepository
    {
        private AccountRepository.Repositories.AccountRepository _accountRepository;
        private readonly List<Operation> _operation;
        private string filePath = @"C:\Users\gioch\source\repos\MiniBank.Data\Account.Data\Operation.xml";



        //transfer, deposit, withdraw

        public OperationRepository(AccountRepository.Repositories.AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
            _operation = LoadFromXml();
        }

        public int Transfer(int fromAccountId, int toAccountId, decimal amount)
        {
            var fromAccount = _accountRepository.GetAccount(fromAccountId);
            var toAccount = _accountRepository.GetAccount(toAccountId);


            if (fromAccount == null || toAccount == null)
                throw new Exception("Account not found");

            if (fromAccount.Balance < amount)
                throw new Exception("Inssuficient balance");

            var operations = LoadFromXml();

            int nextId = operations.Any() ? operations.Max(x => x.Id) + 1 : 1;


            Operation from = new Operation()
            {
                Id = nextId,
                AcountId = fromAccountId,
                Amount = -amount,
                HappendAt = DateTime.Now,
                operationType = OperationType.debit
            };

            Operation to = new Operation()
            {
                Id = nextId,
                AcountId = toAccountId,
                Amount = amount,
                HappendAt = DateTime.Now,
                operationType = OperationType.credit
            };

            operations.Add(from);
            operations.Add(to);

            SaveToXml(operations);

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

            var operations = LoadFromXml();

            int nextId = operations.Any() ? operations.Max(x => x.Id) + 1 : 1;

            Operation operation = new Operation()
            {
                Id = nextId,
                AcountId = accountId,
                Amount = -amount,
                HappendAt = DateTime.Now,
                operationType = OperationType.debit
            };

            operations.Add(operation);
            SaveToXml(operations);

            return 1;

        }

        public int Deposit(int accountId, decimal amount)
        {
            var fromAccount = _accountRepository.GetAccount(accountId);

            if (fromAccount == null)
                throw new Exception("Account not found");

            fromAccount.Balance += amount;

            _accountRepository.UpdateAccount(fromAccount);

            var operations = LoadFromXml();

            int nextId = operations.Any() ? operations.Max(x => x.Id) + 1 : 1;

            Operation operation = new Operation()
            {
                Id = nextId,
                AcountId = accountId,
                Amount = amount,
                HappendAt = DateTime.Now,
                operationType = OperationType.credit
            };

            operations.Add(operation);
            SaveToXml(operations);


            return 1;
        }

        #region Helpres

        private List<Operation> LoadFromXml()
        {
            if (!File.Exists(filePath))
                return new List<Operation>();

            var doc = XDocument.Load(filePath);

            return doc.Root.Elements("Operation")
                .Select(x => new Operation
                {
                    Id = (int)x.Element("Id"),
                    AcountId = (int)x.Element("AccountId"),
                    Amount = (decimal)x.Element("Amount"),
                    HappendAt = (DateTime)x.Element("HappenedAt"),
                    operationType = Enum.Parse<OperationType>(x.Element("Type").Value)
                }).ToList();
        }

        private void SaveToXml(List<Operation> operations)
        {
            var doc = new XDocument(
                new XElement("Operations",
                    operations.Select(op =>
                        new XElement("Operation",
                            new XElement("Id", op.Id),
                            new XElement("AccountId", op.AcountId),
                            new XElement("Amount", op.Amount),
                            new XElement("HappenedAt", op.HappendAt),
                            new XElement("Type", op.operationType.ToString())
                        )
                    )
                )
            );

            doc.Save(filePath);
        }


        #endregion

    }
}
