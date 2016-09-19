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
    public class AccountsService : Service<Accounts>, IAccountsService
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();

        private static IUnitOfWork ut = new UnitOfWork(dbf);
        //public ProductService(IUnitOfWork ut)
        //    : base(ut)
        //{
        //    this.ut = ut;

        //}
        public AccountsService()
           : base(ut)
        { }

        public void AddAccount(Accounts a)
        {
            a.Type = "Current Account";
            ut.getRepository<Accounts>().Add(a);
        }


        public void addAccountBlocked(AccountsBlocked a)
        {
            direction d = ut.getRepository<direction>().GetById(1);

            a.Direction = d;
            a.Blocked = true;
            a.Type = "Blocked Account";
            ut.getRepository<AccountsBlocked>().Add(a);
            ut.getRepository<direction>().Update(d);




        }

        public void addAccountSaving(AccountsSaving a)
        {
            direction d = ut.getRepository<direction>().GetById(1);

            a.Direction = d;
            a.Blocked = true;
            a.Type = "Saving Account";
            ut.getRepository<AccountsSaving>().Add(a);
            ut.getRepository<direction>().Update(d);

        }

        public void BanefitofAccountBlocked()
        {
            DateTime d = new DateTime();
            IEnumerable<AccountsBlocked> accounts = ut.getRepository<AccountsBlocked>().GetMany().ToList();
            d = DateTime.Now;

            foreach (AccountsBlocked a in accounts)
            {
                float? benefit = a.Benefit;
                float? balance = a.AccountBalance + a.AccountBalance * benefit;
                if (d.Day == 1)
                {
                    a.AccountBalance = (float)balance;
                    ut.getRepository<AccountsBlocked>().Update(a);
                    ut.Commit();
                }
            }

        }

        public void BenefitOfAccountSaving()
        {
            DateTime d = new DateTime();
            IEnumerable<AccountsSaving> accounts = ut.getRepository<AccountsSaving>().GetMany().ToList();
            d = DateTime.Now;

            foreach (AccountsSaving a in accounts)
            {
                float benefit = (float)a.Benefit;
                float balance = (float)a.AccountBalance + (float)a.AccountBalance * benefit;
                if (d.Day == 1)
                {
                    a.AccountBalance = (float)balance;
                    ut.getRepository<AccountsSaving>().Update(a);
                    ut.Commit();
                }
            }
        }

        public void DeblockedAcchountBlocked()
        {
            DateTime d = new DateTime();
            IEnumerable<AccountsBlocked> accounts = ut.getRepository<AccountsBlocked>().GetMany().ToList();
            d = DateTime.Now;

            foreach (AccountsBlocked a in accounts)
            {
                if (a.DateDeblocked <= d)
                {
                    a.Blocked = false;
                    ut.getRepository<AccountsBlocked>().Update(a);
                    ut.Commit();
                }
            }

        }

        public void DeblockedAcchountSaving()
        {
            DateTime d = new DateTime();
            IEnumerable<AccountsSaving> accounts = ut.getRepository<AccountsSaving>().GetMany().ToList();
            d = DateTime.Now;

            foreach (AccountsSaving a in accounts)
            {
                if (a.DateofSaving <= d)
                {
                    a.Blocked = false;
                    ut.getRepository<AccountsSaving>().Update(a);
                    ut.Commit();
                }
            }
        }

        public List<Accounts> getAllAccount()
        {
            return ut.getRepository<Accounts>().GetAll().ToList();
        }

        public void GetDetailss(Accounts a)
        {
            throw new NotImplementedException();
            //if (a.GetType() is AccountsBlocked){  }
        }
    }
}
