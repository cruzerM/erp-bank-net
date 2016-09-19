using ERP_BANK.Data.Infrastructure;
using ERP_BANK.Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Service
{
    public class LoanServices : Service<loan>, ILoanServices
    {

        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);

        public LoanServices()
           : base(ut)
        {
        }
        public void CalculateAmountWithInterest(loan loan)
        {
            Double interest;

            interest = (loan.Amount * (double)loan.InterestRate) / 100;

            loan.AmountWithInterest = loan.Amount + interest;

            loan.RestAmount = loan.Amount + interest;

            ut.getRepository<loan>().Update(loan);
            ut.Commit();
        }

        public void CalculateMonthlyPayment(loan loan)
        {
            loan.MonthlyPayment = (double)loan.AmountWithInterest / loan.Period;

            ut.getRepository<loan>().Update(loan);
            ut.Commit();
        }

        public void AddLoanToAccount(loan loan, AccountsCurrent account)
        {
            loan.account = account;

            account.AccountBalance = account.AccountBalance + (float)loan.Amount;
            account.Loans.Add(loan);

            ut.getRepository<loan>().Add(loan);
            ut.getRepository<AccountsCurrent>().Update(account);
            ut.Commit();
        }

        public IEnumerable<loan> GetLoansByAccount(int id)
        {
            return ut.getRepository<AccountsCurrent>().GetById(id).Loans;
        }

        public IEnumerable<loan> GetLoans()
        {
            return ut.getRepository<loan>().GetAll();
        }

        public IEnumerable<AccountsCurrent> GetAccounts()
        {
            return ut.getRepository<AccountsCurrent>().GetAll();
        }

        public IEnumerable<loan_join_account> GetPayBackLogByLoan(int id)
        {
            return ut.getRepository<loan_join_account>().GetMany(g => g.FK_loan == id);
        }

        public AccountsCurrent GetAccount(string libelle)
        {
            return ut.getRepository<AccountsCurrent>().Get(a => a.Libelle.Equals(libelle));
        }

        public AccountsCurrent GetAccountById(int id)
        {
            return ut.getRepository<AccountsCurrent>().GetById(id);
        }

        public void PayBackLoan(loan loan)
        {
            AccountsCurrent account = loan.account;

            loan_join_account join = new loan_join_account();

            if (loan.RestAmount == 0)
            {
                loan.IsPayed = true;

                ut.getRepository<loan>().Update(loan);
            }
            else
            {
                if (account.AccountBalance > loan.MonthlyPayment && loan.RestAmount > 0)
                {
                    account.AccountBalance = account.AccountBalance - (float)loan.MonthlyPayment;

                    loan.RestAmount = loan.RestAmount - loan.MonthlyPayment;

                    loan.IsPayed = false;

                    join.FK_account = loan.account.Id;
                    join.FK_loan = loan.Id;
                    join.date = DateTime.Now;

                    ut.getRepository<loan_join_account>().Add(join);
                    ut.getRepository<AccountsCurrent>().Update(account);
                    ut.getRepository<loan>().Update(loan);

                }
                else
                {
                    Console.WriteLine("Solde insuffisant");
                }

            }

            ut.Commit();

        }
    }
}
