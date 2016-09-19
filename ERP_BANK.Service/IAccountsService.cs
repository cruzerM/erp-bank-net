using ERP_BANK.Domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Service
{
    public interface IAccountsService : IService<Accounts>
    {
        List<Accounts> getAllAccount();
        void GetDetailss(Accounts a);

        void DeblockedAcchountBlocked();

        void DeblockedAcchountSaving();

        void BenefitOfAccountSaving();

        void BanefitofAccountBlocked();

        void addAccountBlocked(AccountsBlocked a);
        void addAccountSaving(AccountsSaving a);
        void AddAccount(Accounts a);
    }
}
