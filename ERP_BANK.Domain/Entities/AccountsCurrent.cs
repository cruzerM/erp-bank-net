using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_BANK.Domain.Entities
{
    public class AccountsCurrent : Accounts
    {
        public virtual ICollection<loan> Loans { get; set; }
        public virtual ICollection<loan_join_account> loan_join_account { get; set; }
        public AccountsCurrent()
        {
            loan_join_account = new List<loan_join_account>();
            Loans = new List<loan>();
        }
    }
}
