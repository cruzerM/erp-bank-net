using ERP_BANK.Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Service
{
    public interface ILoanServices : IService<loan>
    {
        void CalculateAmountWithInterest(loan loan);
        void CalculateMonthlyPayment(loan loan);
        void AddLoanToAccount(loan loan, AccountsCurrent account);
        IEnumerable<loan> GetLoans();
        IEnumerable<AccountsCurrent> GetAccounts();
        IEnumerable<loan> GetLoansByAccount(int id);
        void PayBackLoan(loan loan);
        IEnumerable<loan_join_account> GetPayBackLogByLoan(int id);
        AccountsCurrent GetAccount(string libelle);
        AccountsCurrent GetAccountById(int id);
    }
}
